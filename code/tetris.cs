using Sandbox;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;

namespace Tetris
{
	public partial class TetrisGame : Sandbox.Game
	{
		public TetrisGame()
		{
			if(IsServer)
			{
				Log.Info( "Created Serverside" );
			}
		}
		public override void ClientJoined( Client client )
		{
			var player = new TetrisPlayer();
			client.Pawn = player;
			player.Camera = new TetrisCamera();

			base.ClientJoined( client );
		}
		public override void Simulate( Client cl )
		{
			if ( !cl.Pawn.IsValid() ) return;

			if ( !cl.Pawn.IsAuthority ) return;

			cl.Pawn.Simulate( cl );
		}
		[ServerCmd( "spawn_block" )]
		public static void SpawnTetrisBlocks()
		{
			var entities = All.Where( ( e ) => e is TetrisBlockSpawn );
			Log.Info( "Found " + entities.Count() + " Tetris Spawners" );

			var spawners = new List<Entity>();

			spawners.AddRange( entities );
			
				foreach ( var entity in spawners )
				{
					if ( entity is TetrisBlockSpawn spawner )
					{
						Log.Info( "Spawning " + spawner.Type.ToString() + " Block" );

						var block = new TetrisBlock
						{
							Position = spawner.Position,
						};
					block.PhysicsGroup.Mass = 0.1f;
				}
				}

			
		} 

	}
}
