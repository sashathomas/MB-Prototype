﻿using System;
using Nez;
using Nez.AI.FSM;
using Microsoft.Xna.Framework;
using Nez.Sprites;

namespace NezTest
{
    public class Jumping : State<Player>
    {
        
        public override void Update(float deltaTime)
        {
            
            //_context.GetComponent<SpriteAnimator>().Play("playerJump");
            var moveDir = new Vector2(_context._xAxisInput, 0);

            if (moveDir.X < 0)
            {
                _context.GetComponent<SpriteAnimator>().FlipX = true;
                _context._velocity.X = -_context.moveSpeed;
            }
            else if (moveDir.X > 0)
            {
                _context.GetComponent<SpriteAnimator>().FlipX = false;
                _context._velocity.X = _context.moveSpeed;
            }
            else
            {
                _context._velocity.X = 0;
            }
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
