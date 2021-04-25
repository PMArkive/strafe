using Sandbox;
using Sandbox.Rcon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Strafe.Ply
{
	[Library]
	public class StrafeDuck : NetworkClass
	{
		public BasePlayerController Controller;

		public bool IsActive; // replicate

		public StrafeDuck( BasePlayerController controller )
		{
			Controller = controller;
		}

		public virtual void PreTick() 
		{
			bool wants = Controller.Input.Down( InputButton.Duck );

			if ( wants != IsActive ) 
			{
				if ( wants ) TryDuck();
				else TryUnDuck();
			}

			if ( IsActive )
			{
				Controller.SetTag( "ducked" );
				Controller.ViewOffset *= 0.5f;
			}
		}

		void TryDuck()
		{
			IsActive = true;
		}

		void TryUnDuck()
		{
			var pm = Controller.TraceBBox( Controller.Pos, Controller.Pos, originalMins, originalMaxs );
			if ( pm.StartedSolid ) return;

			IsActive = false;
		}

		// Uck, saving off the bbox kind of sucks
		// and we should probably be changing the bbox size in PreTick
		Vector3 originalMins;
		Vector3 originalMaxs;

		internal void UpdateBBox( ref Vector3 mins, ref Vector3 maxs )
		{
			originalMins = mins;
			originalMaxs = maxs;

			if ( IsActive )
			{
				maxs = maxs.WithZ( 36 );
			}
		}

		//
		// Coudl we do this in a generic callback too?
		//
		public float GetWishSpeed()
		{
			if ( !IsActive )
			{
				return -1;
			}
			return 64.0f;
		}
	}
}
