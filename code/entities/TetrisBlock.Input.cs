﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace Tetris
{
    public partial class TetrisBlock
	{
		public override void Simulate( Client client )
		{
			if ( Input.Pressed( InputButton.Left ) ) // A
			{
				Log.Info( "called" );
				var pos = this.Position;
				Position = new Vector3( pos.x, pos.y + 40, pos.z ).SnapToGrid( 64.0f, true, true, true );
				ResetInterpolation();
			} else if( Input.Pressed(InputButton.Right)) // D
			{
				var pos = this.Position;
				Position = new Vector3( pos.x, pos.y - 40, pos.z ).SnapToGrid( 64.0f, true, true, true );
				ResetInterpolation();
			} else if( Input.Pressed( InputButton.Back ) ) // S
			{
				
			} else if ( Input.Pressed( InputButton.Menu ) ) // Q
			{
				Rotation = Rotation.RotateAroundAxis( Rotation.Backward, 90f );
			} else if ( Input.Pressed( InputButton.Use ) ) // E
			{
				Rotation = Rotation.RotateAroundAxis( Rotation.Forward, 90f );
				Log.Info( "New Rotation: " + Rotation );
			}

				base.Simulate( client );
		}
		/*public override void BuildInput( InputBuilder input )
		{


			

		}
		public void MoveBlock(float velocity)
		{
			velocity = 100;
		}*/
	}
}