using System;
using Nez;
using Nez.AI.FSM;
using Microsoft.Xna.Framework;
using Nez.Sprites;

namespace NezTest
{
    public class Idle : State<Player>
    {
        public override void Update(float deltaTime)
        {
            _context._velocity.X = 0;
            //_context.GetComponent<SpriteAnimator>().Play("playerIdle");
            //_context.animation = "playerIdle";
            if (_context._collisionState.Right || _context._collisionState.Left)
            {
                _context._velocity.X = 0;

            }
            if (_context._collisionState.Above || _context._collisionState.Below)
            {
                _context._velocity.Y = 0;
            }
        }
    }
}
