using Sandbox;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
	[Library("Tetris_Block_Spawn")]
	[Hammer.EditorModel( "models/citizen_props/icecreamcone01.vmdl_c" )]
	[Hammer.Model]
	public partial class TetrisBlockSpawn : ModelEntity
	{
		[Net, Property] public int SpawnerOwner { get; set; }

		public virtual void SetSpawnerOwner(int User)
		{
			if ( SpawnerOwner == 0 )
				SpawnerOwner = User;
		}
	}
}
