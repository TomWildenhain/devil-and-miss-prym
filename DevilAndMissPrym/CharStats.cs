using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevilAndMissPrym
{
	public enum Person{Berta, Stranger, Priest, Mayor, LandOwner, None}
	public enum Skill{Intellect, PeopleSkills, Agility, SelfControl}
	public enum Desire{Greed, Power, Freedom}
	public enum Moral{Good,Evil}
	class CharStats
	{
		const int maxStatVal = 10;
		const int minStatVal = 0;
		private string myFirstName;
		private string myLastName;
		private string myTitle;
		private string myVillage;
		
		private int  myMorality;

		private int myIntellect;
		private int myPeopleSkills;
		private int myAgility;
        private int mySelfControl;

		private int myGreed;
		private int myPower;
		private int myFreedom;
		
		Dictionary<string,string> lineDictionary;
		List<ScoringItem> scoreList;
		private Person myBestFriend;
		private eventsState myEventsState;
		//private Person secondBestFriend;
		public CharStats()
		{
			setDefaultVals();
		}
		public CharStats(string firstName, string lastName, string title)
		{
			setDefaultVals();
			myFirstName = capitalize(firstName);
			myLastName = capitalize(lastName);
			myTitle = capitalize(title);
			
		}
        public CharStats clone()
        {
            CharStats rVal = new CharStats(myFirstName, myLastName, myTitle);
            rVal.myVillage = myVillage;

            rVal.myMorality = myMorality;

            rVal.myIntellect = myIntellect;
            rVal.myPeopleSkills = myPeopleSkills;
            rVal.myAgility = myAgility;
            rVal.mySelfControl = mySelfControl;

            rVal.myGreed = myGreed;
            rVal.myPower = myPower;
            rVal.myFreedom = myFreedom;

            rVal.lineDictionary = lineDictionary.ToDictionary(entry => entry.Key, entry => entry.Value);
            rVal.scoreList = scoreList.ToList<ScoringItem>();
            rVal.myBestFriend = myBestFriend;
            rVal.myEventsState = myEventsState.clone();
            return rVal;
        }
		private void setDefaultVals(){
			myIntellect = 5;
			myPeopleSkills = 5;
			myAgility = 5;
            mySelfControl = 5;

			myGreed = 5;
			myPower = 5;
			myFreedom = 5;
			
			myMorality=0;
			myBestFriend=Person.None;
			lineDictionary=new Dictionary<string, string>();
			scoreList=new List<ScoringItem>();
			myEventsState=new eventsState();
		}
		public void setLine(string key, string line){
			lineDictionary.Add(key,line);
		}
		public string getLine(string key){
			try{
				string rVal=lineDictionary[key];
				if(key!=null){
					return rVal;
				}
			}
			catch{}
			return "[***]";
		}
		private string capitalize(string name)
		{
			if (name.Length > 1)
			{
				return (name[0] + "").ToUpper() + name.Substring(1);
			}
			else
			{
				return name.ToUpper();
			}
		}
		public string getName()
		{
			return myFirstName + " " + myLastName;
		}
		public void updateStat(Desire desire, int delta)
		{
			writeStat(desire, readStat(desire) + delta);
			if (readStat(desire) > maxStatVal)
			{
				writeStat(desire, maxStatVal);
			}
			if (readStat(desire) < minStatVal)
			{
				writeStat(desire, minStatVal);
			}
		}
		public void updateStat(Skill skill, int delta)
		{
			writeStat(skill, readStat(skill) + delta);
			if (readStat(skill) > maxStatVal)
			{
				writeStat(skill, maxStatVal);
			}
			if (readStat(skill) < minStatVal)
			{
				writeStat(skill, minStatVal);
			}
		}
		public void updateStat(Moral moral, int delta){
			if(moral==Moral.Good){
				myMorality+=delta;
			}
			else if(moral==Moral.Evil){
				myMorality-=delta;
			}
			if(myMorality>10){
				myMorality=10;
			}
			if(myMorality<-10){
				myMorality=-10;
			}
		}
		public bool skillCheck(Skill skill, int minVal)
		{
			return readStat(skill) >= minVal;
		}
		public bool moralCheck(Moral moral, int minVal){
			if(moral==Moral.Good){
				return myMorality>=minVal;
			}
			else if(moral==Moral.Evil){
				return -myMorality>=minVal;
			}
			return false;
		}
		public bool desireMoreThan(Desire desire, int minVal){
			return readStat(desire)>=minVal;
		}
		public bool desireLessThan(Desire desire, int maxVal){
			return readStat(desire)<=maxVal;
		}
		public bool isFriendsWith(Person friend){
			return friend==myBestFriend;
		}
		private int readStat(Skill skill)
		{
			if (skill == Skill.Agility) return myAgility;
			if (skill == Skill.Intellect) return myIntellect;
			if (skill == Skill.PeopleSkills) return myPeopleSkills;
			if (skill == Skill.SelfControl) return mySelfControl;
			return -1;
		}
		private int readStat(Desire desire)
		{
			if (desire == Desire.Freedom) return myFreedom;
			if (desire == Desire.Greed) return myGreed;
			if (desire == Desire.Power) return myPower;
			return -1;
		}
		private void writeStat(Skill skill, int value)
		{
			if (skill == Skill.Agility) myAgility = value;
			if (skill == Skill.Intellect) myIntellect = value;
			if (skill == Skill.PeopleSkills) myPeopleSkills = value;
			if (skill == Skill.SelfControl) mySelfControl = value;
		}
		private void writeStat(Desire desire, int value)
		{
			if (desire == Desire.Freedom) myFreedom = value;
			if (desire == Desire.Greed) myGreed = value;
			if (desire == Desire.Power) myPower = value;
		}
		public void updateInOutNames()
		{
			InOut.setNameData(myFirstName, myLastName, myTitle);
		}
		public void setVillage(string village)
		{
			myVillage = capitalize(village);
		}
		public void updateInOutVillage()
		{
			InOut.setVillage(myVillage);
		}
		public void setBestFriend(Person bestFriend){
			myBestFriend=bestFriend;
		}
		private void addScoringItem(int score, string reason){
			scoreList.Add(new ScoringItem(score, reason));
		}
        public void setScoreingItems(bool fired, bool youKilled, bool bertaKilled, bool youArrested, int gold, bool escaped, bool strangerArrested)
        {
            if (fired)
            {
                addScoringItem(-200, "You were fired: ");
            }
            if(youArrested){
                addScoringItem(-400,"You were arrested: ");
            }
            if (youKilled)
            {
                addScoringItem(-1000, "You were killed: ");
            }
            if (bertaKilled)
            {
                addScoringItem(-500, "Berta was killed: ");
            }
            if (strangerArrested)
            {
                addScoringItem(200, "The stranger was arrested: ");
            }
            if (!youKilled && !bertaKilled && !fired && !youArrested)
            {
                addScoringItem(500, "No one was killed: ");
            }
            if (gold == 1 && !youKilled && !fired && !youArrested)
            {
                addScoringItem(300, "You received one bar of gold: ");
            }
            if (gold == 2 && !youKilled && !fired && !youArrested)
            {
                addScoringItem(200, "The village received 10 bars of gold: ");
            }
            if (gold == 3)
            {
                addScoringItem(500, "You received 11 bars of gold: ");
            }
            if (gold == 0)
            {
                addScoringItem(0, "You did not get any gold: ");
            }
            if (desireMoreThan(Desire.Freedom, 7) && !(youKilled || youArrested || gold != 0))
            {
                if (escaped)
                {
                    addScoringItem(200, "You escaped @village: ");
                }
                else
                {
                    addScoringItem(-100, "You are still stuck at @village: ");
                }
            }
        }
		public void printEndStats(){
			InOut.printFullScreen("GAME OVER");
			InOut.printLnSlow("Character stats:");
			int score=myMorality*100;
            InOut.printLnSlow("Morality: " + myMorality * 100 + "/1000");
			foreach (ScoringItem si in scoreList){
				InOut.printLnSlow(si.getReason()+si.getScore());
				score+=si.getScore();
			}
			InOut.printLnSlow("TOTAL: "+score);
		}
		public eventsState getEvents(){
			return myEventsState;
		}
        public bool isKyle()
        {
            return myFirstName == "Kyle";
        }
        
		
	}
}
