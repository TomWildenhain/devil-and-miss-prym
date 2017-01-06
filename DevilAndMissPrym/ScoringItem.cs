/*
 * Created by SharpDevelop.
 * User: bc
 * Date: 5/15/2015
 * Time: 9:17 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace DevilAndMissPrym
{
	/// <summary>
	/// Description of ScoringItem.
	/// </summary>
	public class ScoringItem
	{
		int myScore;
		string myReason;
		public ScoringItem(int score, string reason)
		{
			myScore=score;
			myReason=reason;
		}
		public int getScore(){
			return myScore;
		}
		public string getReason(){
			return myReason;
		}
	}
}
