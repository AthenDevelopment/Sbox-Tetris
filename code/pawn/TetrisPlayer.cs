using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Tetris
{
	public partial class TetrisPlayer : Player
	{
		[Net] public TetrisBlock Block { get; set; }
		public TimeSince TimeSinceSpawn { get; private set; }
		public TetrisPlayer()
		{
			
		Camera = new TetrisCamera();


		}
		public override void Respawn()
		{
			//Camera = new TetrisCamera();

			base.Respawn();
		}
		[Event( "tetris.CreateBlock" )]
		public void CreateBlock( Client cl )
		{
			if ( TimeSinceSpawn > 3 )
			{
				Log.Info( cl );
				var entities = All.Where( ( e ) => e is TetrisBlockSpawn );
				var spawners = new List<Entity>();
				spawners.AddRange( entities );
				foreach ( var entity in spawners )
				{
					if ( entity is TetrisBlockSpawn spawner )
					{
						//Log.Info( "Spawning " + spawner.Type.ToString() + " Block" );

						var block = new TetrisBlock
						{
							Position = spawner.Position.SnapToGrid( 64.0f, true, true, true ),
						};

						block.PhysicsGroup.Mass = 5f;
						block.Tags.Add( "InPlayerUse" );
						cl.Pawn = block;
						TimeSinceSpawn = 0;

					}
				}
			}
		}
		public override void Simulate( Client cl )
		{


			base.Simulate( cl );

			if ( !IsServer ) return;
			if ( Input.Pressed( InputButton.Jump ) )
			{
				Event.Run( "tetris.CreateBlock", cl );
			}
		}
	}
}
