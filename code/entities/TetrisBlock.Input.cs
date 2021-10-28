using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace Tetris
{
    public partial class TetrisBlock
	{
		[Net] public int posY { get; set; }

		public override void Simulate( Client client )
		{
			if ( Input.Pressed( InputButton.Left ) ) // A
			{
				Log.Info( "called" );
				var pos = this.Position;
				if(posY != 512)
				{
					posY = posY + 64;
					Position = new Vector3( pos.x, pos.y + 64, pos.z ).SnapToGrid( 64.0f, true, true, false );
				}
				
				
				ResetInterpolation();
			} else if( Input.Pressed(InputButton.Right)) // D
			{
				var pos = this.Position;
				if ( posY != -512 )
				{
					posY = posY - 64;
					Position = new Vector3( pos.x, pos.y - 64, pos.z ).SnapToGrid( 64.0f, true, true, false );
				}
					
				ResetInterpolation();
			} else if( Input.Pressed( InputButton.Back ) ) // S
			{
				this.PhysicsGroup.Mass = 70f;


			} else if ( Input.Released( InputButton.Back ) )
			{
				this.PhysicsGroup.Mass = 5f;

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
	}
}
