﻿using Strafe.Ply;
using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strafe.UI
{
    public partial class TimerHud : Panel
	{

		public Panel Canvas { get; protected set; }

		private Label _label;

		public TimerHud()
		{
			StyleSheet.Load( "/ui/TimerHud.scss" );

			Canvas = Add.Panel( "timer_hud_canvas" );
			_label = Add.Label(string.Empty, "label");
		}

		public override void Tick()
		{
			base.Tick();

			if(Local.Pawn is not StrafePlayer player || player.Controller == null)
            {
				return;
            }

			var speed = player.HorizontalSpeed;

			if(player.TimerState != TimerState.Running)
            {
				var state = player.TimerState == TimerState.InStartZone
					? "In Start Zone"
					: player.TimerState.ToString();
				_label.Text = $"{state}\n{speed} u/s";
            }
            else
            {
				_label.Text = $"{player.FormattedTimerTime}s\n{speed} u/s\n{player.TimerJumps} jumps\n{player.TimerStrafes} strafes";
			}
		}

	}
}
