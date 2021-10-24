using Sandbox;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Threading;
using Tetris.ui;

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
			

			base.ClientJoined( client );
		}
		public override void Simulate( Client cl )
		{
			if ( !cl.Pawn.IsValid() ) return;

			if ( !cl.Pawn.IsAuthority ) return;

			cl.Pawn.Simulate( cl );
		}

	}
}
