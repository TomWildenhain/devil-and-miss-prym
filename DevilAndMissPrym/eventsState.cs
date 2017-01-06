/*
 * Created by SharpDevelop.
 * User: bc
 * Date: 5/17/2015
 * Time: 4:51 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace DevilAndMissPrym
{
	/// <summary>
	/// Description of eventsState.
	/// </summary>
	public class eventsState
	{
		private bool hasReportedToPolice;
        private bool hasTalkedToFriend;
		public eventsState()
		{
			hasReportedToPolice=false;
            hasTalkedToFriend = false;
		}
        public eventsState clone()
        {
            eventsState rVal = new eventsState();
            rVal.hasReportedToPolice = hasReportedToPolice;
            rVal.hasTalkedToFriend = hasTalkedToFriend;
            return rVal;
        }
		public bool reportedToPolice(){
			return hasReportedToPolice;
		}
		public void reportToPolice(){
			hasReportedToPolice=true;
		}
        public bool talkedToFriend()
        {
            return hasTalkedToFriend;
        }
        public void talkToFriend()
        {
            hasTalkedToFriend = true;
        }
	}
}
