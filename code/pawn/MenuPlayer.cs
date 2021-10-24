using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace Tetris
{
	public partial class MenuPlayer : Player
	{
		public override void Respawn()
		{
			base.Respawn();
			Transmit = TransmitType.Owner;
		}

		public override void ClientSpawn()
		{
			base.ClientSpawn();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
		}
	}
}
