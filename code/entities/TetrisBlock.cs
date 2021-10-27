using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Numerics;
//using Tetris.UI;
namespace Tetris
{
	[Library( "Tetris_Block" )]
	public partial class TetrisBlock : ModelEntity
	{

		[Net] public bool Active {get; private set;}


		public override void Spawn()
		{
			base.Spawn();

			//SetModel( "models/tetris/zsblock.vmdl" ); // d:\steamlibrary\steamapps\common\sbox\addons\sbox-tetris
			Random rnd = new Random();
			//string[] blockModels = { "zsblock.vmdl", "iblock.vmdl", "ljblock.vmdl", "tblock.vmdl", "oblock.vmdl", "" };
			string[] blockModels = { "zBlock", "sBlock", "iBlock", "lBlock", "jBlock", "tBlock", "oBlock" };
			//string[] blockModels = { "lBlock" };
			int bIndex = rnd.Next( blockModels.Length );


			//SetModel( "models/tetris/" + blockModels[bIndex] );


			switch( blockModels[bIndex] )
			{
				case "zBlock":
					SetModel( "models/tetris/zsblock.vmdl" );
					RenderColor = Color.Red;
					Rotation = Rotation.RotateAroundAxis( Rotation.Right, -180f );
					Rotation = Rotation.RotateAroundAxis( Rotation.Backward, 90f );
					break;
				case "sBlock":
					SetModel( "models/tetris/zsblock.vmdl" );
					RenderColor = Color.Green;
					Rotation = Rotation.RotateAroundAxis( Rotation.Backward, -90f );
					break;
				case "iBlock":
					SetModel( "models/tetris/iblock.vmdl" );
					RenderColor = Color.Blue;
					break;
				case "lBlock":
					SetModel( "models/tetris/ljblock.vmdl" );
					Rotation = Rotation.RotateAroundAxis( Rotation.Left, 180f );
					Rotation = Rotation.RotateAroundAxis( Rotation.Forward, 180f );
					RenderColor = Color.Orange;
					break;
				case "jBlock":
					SetModel( "models/tetris/ljblock.vmdl" );
					RenderColor = Color.Magenta;
					break;
				case "tBlock":
					SetModel( "models/tetris/tblock.vmdl" );
					RenderColor = Color.Cyan;
					break;
				case "oBlock":
					SetModel( "models/tetris/oblock.vmdl" );
					RenderColor = Color.Yellow;
					break;
			}

			
			SetupPhysicsFromModel( PhysicsMotionType.Dynamic, false );

			Transmit = TransmitType.Always;

			Predictable = false;
			EnableTouch = true;
		}
		protected override void OnPhysicsCollision( CollisionEventData eventData )
		{
			base.OnPhysicsCollision( eventData );
			if(IsServer)
			{
				if (eventData.Entity.IsWorld && this.Tags.Has( "InPlayerUse" ) && eventData.Entity.ClassInfo.Name != "Tetris_Wall" && eventData.Entity.ClassInfo.Name != "Tetris_Block_Spawn" )
				{
					//DebugOverlay.Sphere( eventData.Pos, 100f, Color.Green, false, 10f );
					//Log.Info( "OnPhysicsCollision" );

					
					this.Tags.Remove( "InPlayerUse" );
					//Log.Info( this.Client.Pawn );
					this.PhysicsGroup.Mass = 20f; // Set Mass on placed block, so blocks don't do a painful slow-mo when falling off the board.
					Event.Run( "tetris.CreateBlockNext", this.Client );
					//this.Client.Pawn = null;

				}


			}
		}
		public override void Touch( Entity other )
		{
			base.Touch( other );
			//Log.Info( other.Name + ": " + other.IsClient );
			if(!other.IsClient && !other.IsWorld && this.Tags.Has( "InPlayerUse" ) && other.ClassInfo.Name != "Delete_Field" && other.ClassInfo.Name != "Tetris_Wall" )
			{
				Log.Info( "Touch" );
				//Log.Info( this.Client.Pawn );
				
				this.Tags.Remove( "InPlayerUse" );
				this.PhysicsGroup.Mass = 20f; // Set Mass on placed block, so blocks don't do a painful slow-mo when falling off the board.
				Event.Run( "tetris.CreateBlockNext", this.Client );
				//this.Client.Pawn = null;
			} else if (other.ClassInfo.Name == "Delete_Field" && this.Tags.Has("InPlayerUse"))
			{
				//this.Client.Pawn = null;
				//Event.Run( "tetris.CreateBlock", this.Client );
				//this.Delete();
			}
		}

	}


}
