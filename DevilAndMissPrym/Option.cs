/*
 * Created by SharpDevelop.
 * User: 16wildenhaint
 * Date: 5/13/2015
 * Time: 1:17 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace DevilAndMissPrym
{
	/// <summary>
	/// Description of Option.
	/// </summary>
	public class Option
	{
		private int myId;
		private string myText;
		public Option(int id, string text)
		{
			myId=id;
			myText=text;
		}
		public int getID(){
			return myId;
		}
		public string getText(){
			return myText;
		}
	}
}
