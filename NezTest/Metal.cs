using System;
using Nez;
using Nez.Tiled;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Nez.AI.FSM;

namespace NezTest
{
    public class Metal : Component, IUpdatable
    {
        public float gravity = 5;
        public bool test = false;
        public float mass = 50f;
        public float length;
        Scene thisScene;

        public float angle;
        public Vector2 newAng;
        public Vector2 oldAng;
        public Vector2 hmm;
        public float allomanticForce = 0f;
        public Vector2 mousePos;
        public Vector2 thisPos;
        public Vector2 _velocity;
        TiledMapMover _mover;
        public BoxCollider _boxCollider;
        public TiledMapMover.CollisionState _collisionState = new TiledMapMover.CollisionState();

        public override void OnAddedToEntity()
        {
            _mover = this.GetComponent<TiledMapMover>();
            _boxCollider = Entity.GetComponent<BoxCollider>();
        }
        public void Update()
        {
            Player player = thisScene.FindEntity("player").GetComponent<Player>();

            hmm.X = (player.Transform.Position.X - Transform.Position.X);
            hmm.Y = (player.Transform.Position.Y - Transform.Position.Y);
            //length = hmm.Length();

            _velocity.Y += gravity * Time.DeltaTime;
            if (_collisionState.Below || _collisionState.Above || _collisionState.Right || _collisionState.Left)
            {
                _velocity = Vector2.Zero;
            }

            

            if (Physics.OverlapRectangle(new RectangleF(this.Transform.Position, this.Transform.Scale)) == thisScene.FindEntity("player").GetComponent<Player>()._boxCollider)
            {
                test = true;
            } else
            {
                test = false;
            }
            
            var distance = Vector2.Distance(this.Transform.Position, player.Transform.Position);

            if (Input.IsKeyPressed(Keys.F) || Input.IsKeyPressed(Keys.E)) // Lock the angle you push from
            {
                angle = (float)Math.Atan2(hmm.Y, hmm.X);
            }

            if (Input.LeftMouseButtonDown || Input.IsKeyDown(Keys.F))
            {
                mousePos = thisScene.Camera.ScreenToWorldPoint(Input.ScaledMousePosition);
                //if (Physics.OverlapRectangle(new RectangleF(mousePos, new Vector2(2, 2))) == _boxCollider)
                //{
                    

                    var ratio = player.mass / (mass + player.mass);
                    var other = 1 - ratio;
                    

                    allomanticForce = (float)(player.burnRate * (Math.Sqrt(mass * player.mass)) / (Math.Pow(distance / 32, 2)));
                    player.allomanticForce = (float)(player.burnRate * (Math.Sqrt(mass * player.mass)) / (Math.Pow(Math.E, -distance / 16)));


                    
                if (allomanticForce > 100)
                {
                    allomanticForce = 100;
                }

                newAng = (allomanticForce * 5) * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                player._velocity += newAng;
                _velocity += -newAng;
                
                //Debug.DrawLine(newAng, player.Transform.Position, Color.Red);
                Debug.DrawLine(this.Transform.Position, player.Transform.Position, Color.Red);

                //if (hmm.X < 0)
                //{
                //player._velocity = new Vector2(other * allomanticForce, other * allomanticForce);
                //_velocity = new Vector2(ratio * -allomanticForce, ratio * -allomanticForce);

                //}
                //else
                //{
                //player._velocity += newAng;
                //player._velocity = new Vector2(other * -allomanticForce, other * -allomanticForce);
                //_velocity = new Vector2(ratio * allomanticForce, ratio * allomanticForce);
                //} 

                //}  
            }

            if (Input.IsKeyDown(Keys.E))
            {
                var ratio = player.mass / (mass + player.mass);
                var other = 1 - ratio;


                allomanticForce = (float)(player.burnRate * (Math.Sqrt(mass * player.mass)) / (Math.Pow(distance / 32, 2)));
                player.allomanticForce = (float)(player.burnRate * (Math.Sqrt(mass * player.mass)) / (Math.Pow(Math.E, -distance / 16)));



                if (allomanticForce > 100)
                {
                    allomanticForce = 100;
                }

                newAng = (allomanticForce * 5) * new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                player._velocity += -newAng;
                _velocity += newAng;
            }
            //length = MathHelper.ToDegrees((float)Math.Atan2(hmm.Y, -hmm.X));
            //_velocity.X += allomanticForce;
            _mover.Move(_velocity * Time.DeltaTime, _boxCollider, _collisionState);
        }

        public Metal(Scene scene)
        {
            thisScene = scene;
        }
    }
}