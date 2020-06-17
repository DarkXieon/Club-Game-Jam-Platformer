using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//[RequireComponent(typeof(Collider2D))]
public class StickyWall : MonoBehaviour
{
    [Header("On Death")]
    public bool StickOnDeath;
    public bool IgnorePhysicsOnDeath;
    
    [Header("Sticky Gravity")]
    public bool SlideDown;

    [Range(0f, 1f)]
    public float GravityMultiplier;

    [Header("Timed Stick")]
    public bool HasStickTimer;
    public float StickLength;

    [Header("Movement Range")]
    public float DropThreshold = .2f;

    private List<StuckPlayer> stuckPlayers;

    private class StuckPlayer
    {
        public Vector2 StartPosition;
        public Rigidbody2D PlayerBody;
        public Die PlayerDeath;
        public AnimationVelocityTracker PlayerAnimationController;
        public Collider2D StuckTo;
        public Collider2D PlayerCollider;
        public float StuckTime;
        public bool HandledDeath;

        public StuckPlayer(Rigidbody2D player, Collider2D hitCollider)
        {
            StartPosition = player.position;
            PlayerBody = player;
            PlayerDeath = player.gameObject.GetComponent<Die>();
            PlayerAnimationController = player.gameObject.GetComponent<AnimationVelocityTracker>();
            StuckTo = hitCollider;
            StuckTime = 0f;

            Collider2D[] colliders = new Collider2D[10];
            player.GetAttachedColliders(colliders);

            PlayerCollider = colliders.Where(collider => collider != null).OrderBy(collider => collider.Distance(hitCollider).distance).First();

            if (PlayerDeath == null)
                Debug.LogError("The script trying to stick to wall has no Die script");

            if (PlayerAnimationController == null)
                Debug.LogError("The script trying to stick to wall has no AnimationVelocityTracker script");
            
            PlayerAnimationController.ForceIdle(true);
        }
    }


    private void Start()
    {
        if (GetComponent<Collider2D>() != null)
        {
            stuckPlayers = new List<StuckPlayer>();
        }
        else
        {
            enabled = false;
            transform
                .GetComponentsInChildren<Collider2D>()
                .Select(collider => collider.gameObject)
                .Distinct()
                .ToList().ForEach(obj =>
                {
                    StickyWall wall = obj.AddComponent<StickyWall>();

                    wall.StickOnDeath = StickOnDeath;
                    wall.IgnorePhysicsOnDeath = IgnorePhysicsOnDeath;

                    wall.SlideDown = SlideDown;

                    wall.GravityMultiplier = GravityMultiplier;

                    wall.HasStickTimer = HasStickTimer;
                    wall.StickLength = StickLength;

                    wall.DropThreshold = DropThreshold;
                });
        }
    }

    private void Update()
    {
        List<StuckPlayer> loopPlayers = stuckPlayers.ToList();

        foreach(StuckPlayer player in loopPlayers)
        {
            player.StuckTime += Time.deltaTime;

            if(IgnorePhysicsOnDeath && player.PlayerDeath.IsDead && !player.HandledDeath)
            {
                player.HandledDeath = true;

                player.PlayerBody.bodyType = RigidbodyType2D.Static;
            }

            if(HasStickTimer && player.StuckTime > StickLength)
            {
                RemovePlayer(player);
            }

            if(!StickOnDeath && player.PlayerDeath.IsDead)
            {
                RemovePlayer(player);
            }

            if(player.StuckTo.Distance(player.PlayerCollider).distance > DropThreshold)
            {
                RemovePlayer(player);
            }
        }
    }
    
    public Action<Rigidbody2D> StickPlayer(Rigidbody2D player, Collider2D hitCollider)
    {
        if(!stuckPlayers.Select(stuck => stuck.PlayerBody).Contains(player))
        {
            stuckPlayers.Add(new StuckPlayer(player, hitCollider));

            player.velocity = Vector2.zero;
            player.transform.parent = transform;
            
            if (SlideDown)
            {
                player.gravityScale = GravityMultiplier;
            }
            else
            {
                player.bodyType = RigidbodyType2D.Kinematic;
            }

            return RemovePlayer;
        }

        return null;
    }

    public void RemovePlayer(Rigidbody2D player)
    {
        RemovePlayer(stuckPlayers.FirstOrDefault(play => play.PlayerBody == player));
    }

    private void RemovePlayer(StuckPlayer player)
    {
        if(player != null)
        {
            player.PlayerBody.gravityScale = 1f;

            player.PlayerAnimationController.ForceIdle(false);

            player.PlayerBody.bodyType = RigidbodyType2D.Dynamic;

            player.PlayerBody.GetComponent<StickToWalls>().UnStick();
            
            player.PlayerBody.transform.localPosition = new Vector3(player.PlayerBody.transform.localPosition.x * 1.1f, player.PlayerBody.transform.localPosition.y);

            player.PlayerBody.transform.parent = null;
            
            stuckPlayers.Remove(player);
        }
    }
}