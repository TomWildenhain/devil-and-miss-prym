/*
 * Created by SharpDevelop.
 * User: 16wildenhaint
 * Date: 5/13/2015
 * Time: 1:08 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

namespace DevilAndMissPrym
{
	/// <summary>
	/// Description of Decision.
	/// </summary>
	public class Decision
	{
		private static string numErrMsg="";
        private static string txtErrMsg = "";
		private string myQuestion;
		List<Option> myOptions;
		public Decision(string question)
		{
			myQuestion=question;
			myOptions=new List<Option>();
		}
		public void addOption(int id, string text){
			myOptions.Add(new Option(id,text));
		}
        public void maybeAddOption(int id, string text, bool condition)
        {
            if (condition) addOption(id, text);
        }
		public int makeDecision(){
			printDecsion();
            int decisionNum = InOut.askForNum(numErrMsg, myOptions.Count);
			return myOptions[decisionNum].getID();
		}
        public string getTextAnswer(int minLength)
        {
            printDecsion();
            return InOut.askForText(txtErrMsg, minLength);
        }
		private void printDecsion(){
			InOut.printLnSlow(myQuestion);
			for(int i=0;i<myOptions.Count;i++){
				InOut.printLnSlow((i+1)+") "+myOptions[i].getText());
			}
		}
        public void addLine(string line)
        {
            myQuestion += "\n" + line;
        }
		public static void setNumErrorMessage(string errrorMessage){
            numErrMsg = errrorMessage;
		}
        public static void setTxtErrorMessage(string errrorMessage)
        {
            txtErrMsg = errrorMessage;
        }
	}
}
