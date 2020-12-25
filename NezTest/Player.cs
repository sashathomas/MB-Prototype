using System;
using Nez;
using Nez.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez.AI.FSM;
using thisSystem = System.Diagnostics;
using Nez.Sprites;

namespace NezTest


{
    public class Player : Component, IUpdatable
    {

        public float moveSpeed = 150;
        public float gravity = 700;
        public float jumpHeight = 16 * 5;

        public string animation;

        public enum Animation
        {
            playerRun,
            playerIdle,
            playerJump,
            playerFall
        }

        public float mass = 50f;
        public float allomanticForce = 0f;
        public float burnRate = 0.5f;

        public StateMachine<Player> StateMachine;
        TiledMapMover _mover;
        public BoxCollider _boxCollider;
        public Vector2 _velocity;
        public SpriteAnimator _animator;

        Scene thisScene;
        SpriteAtlas atlas;
        Camera camera;

        public VirtualIntegerAxis _xAxisInput;
        VirtualButton _jumpInput;
        public TiledMapMover.CollisionState _collisionState = new TiledMapMover.CollisionState();

        
        public override void OnAddedToEntity()  
        {
            _mover = this.GetComponent<TiledMapMover>();
            _boxCollider = Entity.GetComponent<BoxCollider>();

            _xAxisInput = new VirtualIntegerAxis();
            _xAxisInput.Nodes.Add(new VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Microsoft.Xna.Framework.Input.Keys.A, Microsoft.Xna.Framework.Input.Keys.D));

            _jumpInput = new VirtualButton();
            _jumpInput.Nodes.Add(new VirtualButton.KeyboardKey(Microsoft.Xna.Framework.Input.Keys.Space));

            _animator = Entity.AddComponent<SpriteAnimator>();
            atlas = thisScene.Content.LoadSpriteAtlas("Content/out.atlas");
            _animator.AddAnimationsFromAtlas(atlas);
            camera = this.GetComponent<Camera>();

        }

        void IUpdatable.Update()
        {
            var moveDir = new Vector2(_xAxisInput, 0);
            animation = "playerIdle";

            if (moveDir.X != 0 && _collisionState.Below) // moving to the left and right
            {
                animation = "playerRun";    
                StateMachine.ChangeState<Running>();
            } 
            else
            {
                animation = "playerIdle";
                StateMachine.ChangeState<Idle>();
            }

            if (_collisionState.Below && _jumpInput.IsPressed)
            {
                _velocity.Y = -Mathf.Sqrt(2f * jumpHeight * gravity);
                animation = "playerJump";
                StateMachine.ChangeState<Jumping>();
            }

            if (_velocity.Y > 0)
            {
                animation = "playerFall";
                StateMachine.ChangeState<Falling>();
            }
            if (_velocity.Y < 0)
            {
                StateMachine.ChangeState<Jumping>();
            }

            _velocity.Y += gravity * Time.DeltaTime;

            if (!_animator.IsAnimationActive(animation))
            {
                _animator.Play(animation);
            }

            //_velocity.X += allomanticForce;
            _mover.Move(_velocity * Time.DeltaTime, _boxCollider, _collisionState);
            
            StateMachine.Update(Time.DeltaTime);
            if (Input.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.X))
            {
                _velocity.Y = 0;
                thisScene.Camera.ZoomIn(0.1f);
            }

        }

        public Player(Scene scene)
        {
            thisScene = scene;
            StateMachine = new StateMachine<Player>(this, new Idle());
            StateMachine.AddState(new Idle());
            StateMachine.AddState(new Running());
            StateMachine.AddState(new Jumping());
            StateMachine.AddState(new Falling());
        }
    }
}
