using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Tetris.UI;
namespace Tetris
{
	public partial class TetrisPlayer : Player
	{
		[Net] public TetrisBlock Block { get; set; }
		public TimeSince TimeSinceSpawn { get; private set; }
		[Net] public int PlayerID { get; set; } = -1;
		[Net] public BaseRound baseRound { get; set; }

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
		public void CreateBlock( )
		{
			var spawns = Entity.All.OfType<TetrisBlockSpawn>().ToList();
			var players = Client.All.Select( ( client ) => client.Pawn as Player ).ToList();

			foreach ( var player in players )
			{
				if ( spawns.Count > 0 )
				{
					var spawn = spawns[0];
					spawns.RemoveAt( 0 );
					//spawn.SpawnerOwner = player.Client.UserId;
					spawn.Owner = player.Owner;
					Log.Info( spawn.Owner );
					if ( spawn.Owner == player.Owner )
					{
						var block = new TetrisBlock
						{
							Position = spawn.Position.SnapToGrid( 64.0f, true, true, true ),
						};

						block.PhysicsGroup.Mass = 5f;
						block.Tags.Add( "InPlayerUse" );

						player.Client.Pawn = block;
						TimeSinceSpawn = 0;
					}



					//TimeSinceSpawn = 0;
				}
			}
		}
		[Event( "tetris.CreateBlockNext" )]
		public void CreateBlockAfterTouch(Client cl)
		{
				if ( TimeSinceSpawn > 3 )
				{
					var GetSpawnPoint = Entity.All.OfType<TetrisBlockSpawn>().ToList().Find( e => e.Owner == cl.Pawn.Owner );
					Log.Info( GetSpawnPoint );
					var block = new TetrisBlock
					{
						Position = GetSpawnPoint.Position.SnapToGrid( 64.0f, true, true, true ),
					};

					block.PhysicsGroup.Mass = 5f;
					block.Tags.Add( "InPlayerUse" );

					GetSpawnPoint.Client.Pawn = block;
					TimeSinceSpawn = 0;

				}
		}
		
		
	}
}
