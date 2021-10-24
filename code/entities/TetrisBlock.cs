using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tetris
{
	[Library( "Tetris_Block" )]
	public partial class TetrisBlock : ModelEntity
	{

		[Net] public TetrisBlockType Type { get; private set; }
		[Net] public bool Active {get; private set;}


		public override void Spawn()
		{
			base.Spawn();

			//SetModel( "models/tetris/zsblock.vmdl" ); // d:\steamlibrary\steamapps\common\sbox\addons\sbox-tetris
			Random rnd = new Random();
			string[] blockModels = { "zsblock.vmdl", "iblock.vmdl", "ljblock.vmdl", "tblock.vmdl" };

			int bIndex = rnd.Next( blockModels.Length );

			SetModel( "models/tetris/" + blockModels[bIndex] );


			switch( blockModels[bIndex])
			{
				case "zsblock.vmdl":
					RenderColor = Color.Green;
					break;
				case "iblock.vmdl":
					RenderColor = Color.Cyan;
					break;
				case "ljblock.vmdl":
					RenderColor = Color.Orange;
					break;
				case "tblock.vmdl":
					RenderColor = Color.Red;
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
				if (eventData.Entity.IsWorld && this.Tags.Has( "InPlayerUse" ) && eventData.Entity.ClassInfo.Name != "Tetris_Wall" )
				{ 
					//DebugOverlay.Sphere( eventData.Pos, 100f, Color.Green, false, 10f );

					this.Client.Pawn = null;
					this.Tags.Remove( "InPlayerUse" );
					Log.Info( this.Client.Pawn );
					this.PhysicsGroup.Mass = 20f; // Set Mass on placed block, so blocks don't do a painful slow-mo when falling off the board.
					Event.Run( "tetris.CreateBlock", this.Client );
					
				}


			}
		}
		public override void Touch( Entity other )
		{
			base.Touch( other );
			//Log.Info( other.Name + ": " + other.IsClient );
			if(!other.IsClient && !other.IsWorld && this.Tags.Has( "InPlayerUse" ) && other.ClassInfo.Name != "Delete_Field" && other.ClassInfo.Name != "Tetris_Wall" )
			{
				//Log.Info( this.Client.Pawn );
				this.Client.Pawn = null;
				this.Tags.Remove( "InPlayerUse" );
				this.PhysicsGroup.Mass = 20f; // Set Mass on placed block, so blocks don't do a painful slow-mo when falling off the board.
				Event.Run( "tetris.CreateBlock", this.Client );
			} else if (other.ClassInfo.Name == "Delete_Field" && this.Tags.Has("InPlayerUse"))
			{
				//this.Client.Pawn = null;
				//Event.Run( "tetris.CreateBlock", this.Client );
				//this.Delete();
			}
		}

	}


}
