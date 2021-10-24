using System.Linq;
using Sandbox;

namespace Tetris
{
	public struct TetrisArena
	{
		public Vector3 CameraPos
		{
			get
			{
				return Entity.All.First( e => e.Name.Equals( "camera_01" ) ).Position;
			}
		}
		public Rotation CameraRot
		{
			get
			{
				return Entity.All.First( e => e.Name.Equals( "camera_01" ) ).Rotation;
			}
		}
	}
}
