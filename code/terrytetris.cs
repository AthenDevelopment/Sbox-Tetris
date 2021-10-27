using Sandbox;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
//using Tetris.ui;
using Tetris;

namespace Tetris
{
	public partial class TetrisGame : Sandbox.Game
	{
		private int playerid;
		[Net] public TetrisBlockSpawn SpawnerOwner { get; set; }
		[Net] public BaseRound Round { get; private set; }
		public TetrisGame()
		{
			if(IsServer)
			{
				Log.Info( "Created Serverside" );
				
			}
			if(IsClient)
			{
				//new Hud();
			}
		}
		public override void ClientJoined( Client client )
		{
			var player = new TetrisPlayer();
			client.Pawn = player;
			player.Camera = new TetrisCamera();

			playerid++;
			player.PlayerID = playerid;

			assignPlayArea( client );




			base.ClientJoined( client );
		}
		public void assignPlayArea( Client cl)
		{
			var spawns = All.OfType<TetrisBlockSpawn>().ToList();
			Log.Info( spawns );


			/*
			var entities = All.Where( ( e ) => e is TetrisBlockSpawn );
			var test = TetrisBlockSpawn.All;
			var spawners = new List<TetrisBlockSpawn>();
			spawners.Add( test );
			foreach( var ent in spawners)
			{
				Log.Info( ent );
			}*/
			//var entities = spawners.Where( e => e.SpawnerOwner == 0 ).FirstOrDefault();
			//Log.Info( entities );
			//var selectedSpawner = spawners.SkipWhile( i => i.SpawnerOwner != 0).Skip( 1 ).FirstOrDefault();
			//Log.Info( entities );
			//entities.SpawnerOwner = cl.UserId;
			//	Log.Info( entities.SpawnerOwner );
			
			//selectedSpawner = cl.Pawn;




		}
		[Event("tetris.ChangeRound")]
		public void ChangeRound(BaseRound round)
		{
			Assert.NotNull( round );
			Round?.Finish();
			Round = round;
			Round?.Start();
			
		}

		[ServerCmd( "StartTetris" )]
		public static void StartTetrisTest()
		{
			Event.Run( "tetris.ChangeRound", new PlayRound() );
		}


		public override void Simulate( Client cl )
		{
			if ( !cl.Pawn.IsValid() ) return;

			if ( !cl.Pawn.IsAuthority ) return;

			cl.Pawn.Simulate( cl );
		}

	}
}
