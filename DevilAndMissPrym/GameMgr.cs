using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DevilAndMissPrym
{
	class GameMgr
	{
		private Decision currentD;
		private CharStats currentStats;
		private string pauseMessage;
		private Random rand=new Random();
        private CharStats backupStats;
		public GameMgr()
		{
			Decision.setNumErrorMessage("Sorry, that was not an option.");
			Decision.setTxtErrorMessage("Sorry, that is not a valid input.");
			pauseMessage="Press enter to continue...";
            bool doStats = true;
            while (true)
            {
                runGame(doStats);
                int selection = 0;
                currentD = new Decision("Would you like to play again");
                currentD.addOption(1,"Play again.");
                currentD.addOption(2, "Play again with same character.");
                currentD.addOption(3, "Exit.");
                selection = currentD.makeDecision();
                if (selection == 1)
                {
                    doStats = true;
                }
                else if (selection == 2)
                {
                    doStats = false;
                }
                else if (selection == 3)
                {
                    break;
                }
            }
		}
		private void runGame(bool doStats)
		{
            if (doStats)
            {
                part0_StatQuiz();
                backupStats = currentStats.clone();
            }
            else
            {
                currentStats = backupStats.clone();
            }
			int result;
			result=part1_GoToForest();
			if(result==1){
				part2_Forest();
			}
			else if(result==2){
				part2a2_GetKilled();
			}
			pause();
		}
		private void part0_StatQuiz()
		{
			int selection = 0;
			string answer = "";
			currentD = new Decision("What is your first name?");
			string fName = currentD.getTextAnswer(1);
			currentD = new Decision("Last name?");
			string lName = currentD.getTextAnswer(1);
			currentD = new Decision("Title? (Mr./Mrs./etc.)");
			string title = currentD.getTextAnswer(2);
			currentStats = new CharStats(fName, lName, title);
			currentStats.updateInOutNames();
			InOut.printStartLogo();
			currentD = new Decision("Welcome @fname, what is the name of the city/village you live in?");
			answer = currentD.getTextAnswer(2);
			currentStats.setVillage(answer);
			currentStats.updateInOutVillage();
			currentD = new Decision("You live in the small village of @village. Its 281 inhabitants " +
									"live simply without modern conveniences. " +
									"What do you like most about @village?");
			currentD.addOption(1, "The people. I like being able to know each person in the village by name.");
			currentD.addOption(2, "The lifestyle. I like the traditional way of life without the burden of technology.");
			currentD.addOption(3, "The peacefulness. I am able to find time to be alone and think.");
			currentD.addOption(4, "I hate @village! I would leave if I had the chance.");
			selection = currentD.makeDecision();
			if (selection == 1)
			{
				currentStats.updateStat(Skill.PeopleSkills, 2);
			}
			else if (selection == 2)
			{
				currentStats.updateStat(Desire.Freedom, -2);
			}
			else if (selection == 3)
			{
				currentStats.updateStat(Skill.SelfControl,2);
			}
			else if (selection == 4)
			{
				currentStats.updateStat(Desire.Freedom, 2);
			}
			currentD = new Decision("When you are not busy working at @village's hotel as a bar tender, what do you like to do?");
			currentD.addOption(1, "Read books.");
			currentD.addOption(2, "Target practice.");
			currentD.addOption(3, "Fish.");
			currentD.addOption(4, "Meditate.");
			selection = currentD.makeDecision();
			if (selection == 1)
			{
				currentStats.updateStat(Skill.Intellect, 2);
				currentStats.setLine("hobby","reading this book.\" (continues reading)");
			}
			else if (selection == 2)
			{
				currentStats.updateStat(Skill.Agility, 2);
				currentStats.setLine("hobby","practicing my aim.\" (continues target practice)");
			}
			else if (selection == 3)
			{
				currentStats.updateStat(Skill.Agility, 1);
				currentStats.updateStat(Skill.SelfControl, 1);
				currentStats.setLine("hobby","fishing.\" (continues fishing)");
			}
			else if (selection == 4)
			{
				currentStats.updateStat(Skill.SelfControl, 3);
				currentStats.setLine("hobby","meditating.\" (closes eyes and ignores Carlos)");
			}
			currentD = new Decision("If you could escape your routine life in @village, which of the " +
									"following would you most like to become?");
			currentD.addOption(1, "The mayor of a large city.");
			currentD.addOption(2, "A wealthy business owner.");
			currentD.addOption(3, "I would travel the world and visit different countries.");
			selection = currentD.makeDecision();
			if (selection == 1)
			{
				currentStats.updateStat(Desire.Power, 2);
			}
			else if (selection == 2)
			{
				currentStats.updateStat(Desire.Greed, 2);
			}
			else if (selection == 3)
			{
				currentStats.updateStat(Desire.Freedom, 1);
			}
			InOut.printLnSlow("Notable people living in @village:");
			InOut.printLnSlow("The Priest: Once the pastor of an important parish, the priest was " +
							 "transferred to @village by the new bishop because of " +
							 "rumors that he was not properly distributing his wealth.  He wishes that " +
							 "more people would practice their Catholic faith and attend Mass " +
							 "instead of basing their beliefs on old Celtic legends.",10);
			pause();
			InOut.printLnSlow("The Mayor: Realizing that @village may soon be taken over by industrial " +
							 "companies, the mayor is desperate to secure the village's future. " +
							 "At one point he promoted the building of a playground to attract children " +
							 "to @village, even though there currently are none who would use it.",10);
			pause();
			InOut.printLnSlow("Old Berta: ever since her husband died in an accident, Berta does nothing " +
							 "except sit outside her house and observe the view.  Some people believe " +
							 "that she is a witch and can communicate with the dead.  She is either " +
							 "extremely wise, or somewhat crazy.",10);
			pause();
			currentD=new Decision("\nIf you needed advice about an important issue, who would you be most " +
							 "likely to consult?");
			currentD.addOption(1,"The Priest: He is a holy man and could help me make the best decision.");
			currentD.addOption(2,"The Mayor: He is the figure of authority and should know about" +
							   "any important issue that may effect @village.");
			currentD.addOption(3,"Berta: She is very wise would know what I should do.");
			selection=currentD.makeDecision();
			if(selection==1){
				currentStats.setBestFriend(Person.Priest);
				currentStats.updateStat(Moral.Good,1);
			}
			else if(selection==2){
				currentStats.setBestFriend(Person.Mayor);
			}
			else if(selection==3){
				currentStats.setBestFriend(Person.Berta);
			}
		}
		private void test0_SkipQuize(){
			currentStats=new CharStats("[First]","[Last]", "[Title]");
			currentStats.setVillage("[Village]");
			currentStats.setLine("hobby","meditating.\" (closes eyes and ignores Carlos)");
			currentStats.updateInOutNames();
			currentStats.updateInOutVillage();
			
			currentStats.updateStat(Skill.Agility,0);
			currentStats.updateStat(Skill.Intellect,0);
			currentStats.updateStat(Skill.PeopleSkills,0);
			currentStats.updateStat(Skill.SelfControl,0);
			
			currentStats.updateStat(Desire.Freedom,0);
			currentStats.updateStat(Desire.Greed,0);
			currentStats.updateStat(Desire.Freedom,0);
			
			currentStats.updateStat(Moral.Good,0);
			
			currentStats.setBestFriend(Person.Mayor);
		}
		private int part1_GoToForest()
		{
			int selection = 0;
			currentD = new Decision("A stranger named Carlos has come to visit @village.  " +
									"After signing into the hotel, he approaches you and says:");
			currentD.addLine("\"Hello, I'd like you to come and look at something.\"");
			currentD.addOption(1, "\"Ok, where is it?\"");
            currentD.maybeAddOption(1, "\"What a day to be alive!\"", currentStats.isKyle());
			currentD.addOption(2, "\"Sorry, Carlos, I am busy "+currentStats.getLine("hobby"));
			currentD.addOption(3, "\"I have already seen everything there is to see in @village.\"");
			selection = currentD.makeDecision();
			if (selection == 1)
			{
				return 1;
			}
			else if (selection == 2)
			{
				currentD = new Decision("\"My name isn't Carlos, and everything I wrote on the form at the hotel is false.\"");
				currentD.addOption(1, "I decide to follow him.");
				currentD.addOption(2, "\"Who are you?\"");
				currentD.addOption(3, "\"I will report you to the police for using a false identity.\"");
				selection = currentD.makeDecision();
				if (selection == 1)
				{
					return 1;
				} 
				else if (selection == 2)
				{
					return part1_sub1_WhoAreYou();
				}
				else if(selection==3){
					currentStats.updateStat(Skill.Intellect,-3);
					currentD=new Decision("\"You have no evidence that the information I provided was false " +
										  "and it is unlikely that the police would believe your word over mine.  " +
										  "I really do want to show you something.\"");
					currentD.addOption(1,"I decide to follow him.");
					currentD.addOption(2,"I turn around and leave.");
					selection=currentD.makeDecision();
					if(selection==1){
						return 1;
					}
					else if(selection==2){
						return 2;
					}
				}
			}
			else if (selection == 3)
			{
				currentD = new Decision("\"It's not @village I want to show you. It's something you've never seen before.\"");
				currentD.addOption(1, "I decide to follow him.");
				currentD.addOption(2, "\"Who are you?\"");
				selection = currentD.makeDecision();
				if (selection == 1)
				{
					return 1;
				}
				else if (selection == 2)
				{
					return part1_sub1_WhoAreYou();
				}
			}
			return returnError();
		}
		private int part1_sub1_WhoAreYou()
		{
			int selection = 0;
			currentD = new Decision("I will answer all your questions, but first you have to come with me.");
			currentD.addOption(1, "I decide to follow him.");
			currentD.addOption(2, "I turn around and leave.");
			selection = currentD.makeDecision();
			if (selection == 1)
			{
				return 1;
			}
			else if (selection == 2)
			{
				return 2;
			}
			return returnError();
		}
		private int part2a2_GetKilled(){
			int selection=0;
			currentD=new Decision("What do you do next?");
			currentD.addOption(1,"I return to my home");
			currentD.addOption(2,"I use the phone in the hotel to call the police an report "+
							   "the stranger as a suspicious person.");
			selection=currentD.makeDecision();
			if(selection==1){
				//currentStats.addScoringItem(-200, "You were fired: ");
                currentStats.setScoreingItems(true, false, false, false, 0, false,false);
				endGame("The stranger tells the hotel landlady about your rude behavior.  " +
						"She decides to fire you.");
				return 1;
			}
			else if(selection==2){
				//currentStats.addScoringItem(-200, "You were fired: ");
				//currentStats.addScoringItem(-1000, "You were killed: ");
                currentStats.updateStat(Moral.Good, 1);
                currentStats.setScoreingItems(true, true, false, false, 0, false,false);
				InOut.printLnSlow("You call the police and tell them about the incident.  They thank you " +
						"for the report but decide not to investigate due to lack of evidence.  " +
						"You later see the stranger and the landlady talking.  The landlady listened " +
						"to your phone call and decides to fire you for embarrassing the village.");
				pause();
				InOut.printLnSlow("Soon the rest of the village hears about the incident.  " +
						"In addition, the stranger has told them something else.");
				pause();
				endGame("One day, the villagers surround you and use their riffles to kill you.");
				return 2;
			}
			return returnError();
		}
		private int part2_Forest(){
			int selection=0;
			currentD=new Decision("You follow the Stranger into the forest.  He shows you a gold bar.");
			currentD.addLine("He says: \"I am performing an experiment to determine if humans are fundamentally good or evil.  " +
			                 "\"If you take this bar you will be breaking the commandment " +
							 "\"Thou shalt not steal.\" \"");
			currentD.addOption(2,"I continue listening.");
			currentD.addOption(1,"I take the bar and run.");
			selection=currentD.makeDecision();
			if(selection==1){
				return part7_StealGold(true,false);//Take the gold while he is watching
			}
			else if(selection==2){
				currentD=new Decision("The stranger shows you 10 more gold bars.");
				currentD.addLine("He says: \"I want the village to break the commandment \"Thou shalt not kill.\"  " +
				                 "I'm giving them a week. If, at the end of seven days, someone in the village is found dead, " +
				                 "then the money will go to the other villagers\"");
				currentD.addOption(2, "I continue listening.");
				currentD.maybeAddOption(1, "I turn around and leave in disgust.",
				                        currentStats.desireLessThan(Desire.Greed,6)&&currentStats.desireLessThan(Desire.Power,6));
				selection=currentD.makeDecision();
				if(selection==1){
					return part2a2_GetKilled();
				}
				else{
					InOut.printLnSlow("I want you to tell them about this new opportunity.  You may decide not to cooperate, " +
					                  "in which case, I'll tell everyone that I gave you the chance to help them, but you refused, " +
					                  "and then I'll put my proposition to them myself. If they do decide to kill someone, you " +
					                  "will probably be their chosen victim.");
					pause();
					InOut.printLnSlow("I will only be here for a week.");
					pause();
					return part3_MainDecisions();
				}
			}
			return returnError();
		}
		private int part3_MainDecisions(){
			int day=1;
			int result=0;
			int breakNum=3;
			bool knowsMeeting=false;
			for(day=1;day<=7;day++){
				InOut.printFullScreen("DAY "+day);
				result=part4_sub1_doDay(day,true,knowsMeeting);
				if(result==breakNum){
					return result;
				}
				result=part4_DayDecision(day,false);
				if(result==breakNum){
					return result;
				}
				result=part4_sub1_doDay(day,false,knowsMeeting);
				if(result==breakNum){
					return result;
				}
				if(day==4){
					knowsMeeting=result==1;
				}
			}
			return returnError();
		}
		private int part4_DayDecision(int day,bool noHappyEnding){
			int selection=0;
			currentD=new Decision("What would you like to do today?");//police, tell village, tell/consult friend, wait, take goldee 
			currentD.addOption(1,"Wait and see what happens.");
            currentD.maybeAddOption(2, "Report the stranger to the police.",!currentStats.getEvents().reportedToPolice());
            currentD.maybeAddOption(3, "Tell the village about the stranger's offer.", day <= 5 && !noHappyEnding);
			currentD.maybeAddOption(4,"Take the gold.",day!=3);
            currentD.maybeAddOption(5, "Visit Berta's house.", currentStats.isFriendsWith(Person.Berta) && !currentStats.getEvents().talkedToFriend());
            currentD.maybeAddOption(6, "Talk to the mayor.", currentStats.isFriendsWith(Person.Mayor) && !currentStats.getEvents().talkedToFriend());
            currentD.maybeAddOption(7, "Consult the Priest.", currentStats.isFriendsWith(Person.Priest) && !currentStats.getEvents().talkedToFriend());
			selection=currentD.makeDecision();
			if(selection==1){
				return 1;
			}
			else if(selection==2){
				return part7_ReportToPolice();//Report to police
			}
			else if(selection==3){
				return part5_tellVillageAboutOffer(day);
			}
			else if(selection==4){
				return part7_StealGold(false,false);//Take gold stranger does not know
			}
			else if(selection==5){
				return part7_TalkToBerta();//She increases stats
			}
			else if(selection==6){
                if (day <= 5 && !noHappyEnding)
                {
					return part7_TalkToMayor(false);//Maybe encourage, maybe tell him
				}
				else{
                    return part7_TalkToMayor(true);//Has already decided to kill you
				}
			}
			else if(selection==7){
                if (day <= 5 && !noHappyEnding)
                {
					return part7_TalkToPriest(false);//Maybe encourage, maybe tell him (like mayor)
				}
				else{
                    return part7_TalkToPriest(true);//Has already decided to kill you
				}
			}
			return returnError();
		}
		private int part4_sub1_doDay(int day, bool morning, bool knowsMeeting){
			if(day==1&&!morning){
				return part4_sub1_doDay1();
			}
			else if(day==2&&!morning){
				return part4_sub1_doDay2();
			}
			else if(day==3&&morning){
				return part4_sub1_doDay3m();
			}
			else if(day==3&&!morning){
				return part4_sub1_doDay3e();
			}
			else if(day==4&&morning){
				return part4_sub1_doDay4m();
			}
			else if(day==4&&!morning){
				return part4_sub1_doDay4e();
			}
			else if(day==5&&morning){
				return part4_sub1_doDay5m(knowsMeeting);
			}
			else if(day==5&&!morning){
				return part4_sub1_doDay5e();
			}
			else if(day==6&&morning){
				return part4_sub1_doDay6m();
			}
			else if(day==6&&!morning){
				return part4_sub1_doDay6e();
			}
			return 1;
		}
		private int part4_sub1_doDay1(){
			int selection=0;
			InOut.printLnSlow("You work at the bar and note how the stranger interacts with the other villagers.  " +
			                  "He tells stories and buys them drinks, making him very popular.");
			pause();
			currentD=new Decision("That night, you cannot sleep.  What do you do?");
			currentD.addOption(1, "I try to pray.");
			currentD.addOption(2, "I think about the gold and what I could use it for.");
			currentD.addOption(3, "I think about the stranger and why he would want the village to kill someone.");
			currentD.addOption(4, "I think about who the best person to kill would be.");
			selection=currentD.makeDecision();
			if(selection==1){
				currentStats.updateStat(Moral.Good,3);
			}
			else if(selection==2){
				currentStats.updateStat(Desire.Greed,1);
				currentD=new Decision("What would you use the gold for?");
				currentD.addOption(1,"I would use my bar of gold to escape @village.");
				currentD.addOption(2,"I would give my bar to @village to persuade the mayor to follow my agenda.");
				currentD.addOption(3,"I would convert my bar into cash and be the richest person in @village.");
				selection=currentD.makeDecision();
				if(selection==1){
					currentStats.updateStat(Desire.Freedom,1);
				}
				else if(selection==2){
					currentStats.updateStat(Desire.Power,1);
				}
				else if(selection==3){
					currentStats.updateStat(Desire.Greed,1);
				}
			}
			else if(selection==3){
				currentStats.updateStat(Skill.PeopleSkills,1);
			}
			else if(selection==4){
				currentStats.updateStat(Moral.Evil,3);
                currentD = new Decision("Who do you think would be the best person to kill?");
                currentD.addOption(1, "Berta.  She does not contribute to the village and will die soon anyway.");
                currentD.addOption(2, "The priest.  He is power hungry and corrupt");
                currentD.addOption(3, "The mayor.  Then we would be able to elect a new one.");
                currentD.addOption(4, "The hotel landlady.  She deserves it for paying me too little.");
                selection = currentD.makeDecision();
                if (selection == 1)
                {
                    currentStats.updateStat(Skill.Intellect, 1);
                }
                else if (selection == 2 || selection == 3)
                {
                    currentStats.updateStat(Desire.Power, 2);
                }
                else if (selection == 4)
                {
                    currentStats.updateStat(Desire.Greed, 1);
                }
			}
			return 1;
		}
		private int part4_sub1_doDay2(){
			int selection=0;
			InOut.printLnSlow("In the bar that night, the stranger notes how quiet the children are.  " +
			                  "There is an awkward silence for there are no children in @village.");
			pause();
			currentD=new Decision("You have difficulty sleeping and keep thinking about the gold.  Should you take it?");
			currentD.maybeAddOption(1, "Yes, it will help me to escape and the village doesn't need me anyway.",
			                        currentStats.desireMoreThan(Desire.Freedom,4));
			currentD.maybeAddOption(2, "Yes, it would give me financial security and allow you to quit my job.",
			                        currentStats.desireMoreThan(Desire.Greed,5));
			currentD.maybeAddOption(3, "Yes, it would give me a means to become a more important member of the village.",
			                        currentStats.desireMoreThan(Desire.Power,5));
			currentD.addOption(4, "Yes, the stranger does not deserve the gold.");
			currentD.maybeAddOption(5, "No, it would be immoral because it is the stranger's gold.",
			                        currentStats.moralCheck(Moral.Good,2));
			currentD.addOption(6, "No, it is too risky.  I would get caught.");
			selection=currentD.makeDecision();
			if(selection==1){
				currentStats.updateStat(Desire.Freedom,1);
				currentStats.updateStat(Desire.Greed,1);
			}
			else if(selection==2){
				currentStats.updateStat(Desire.Greed,2);
			}
			else if(selection==3){
				currentStats.updateStat(Desire.Power,1);
				currentStats.updateStat(Desire.Greed,1);
			}
			else if(selection==4){
				currentStats.updateStat(Moral.Evil,1);
				currentStats.updateStat(Desire.Greed,1);
			}
			else if(selection==5){
				currentStats.updateStat(Moral.Good,1);
				currentStats.updateStat(Desire.Greed,-1);
			}
			else if(selection==6){
				currentStats.updateStat(Skill.Intellect,1);
				currentStats.updateStat(Desire.Greed,-1);
			}
			return 1;
		}
		private int part4_sub1_doDay3m(){
			int selection=0;
			currentD=new Decision("In the morning, you consider going to the forest and looking " +
			                      "at the gold bar the stranger showed you.  What do you do?");
			currentD.addOption(1,"I decide look at the gold bar to see if it is real.");
			currentD.addOption(2,"I go and examine the bar to see how much it is worth.");
			currentD.maybeAddOption(3,"I take the gold bar.",currentStats.desireMoreThan(Desire.Greed,7));
			currentD.maybeAddOption(4,"I do not visit the forest.",currentStats.moralCheck(Moral.Good,0)&&currentStats.desireLessThan(Desire.Greed,5));
			selection=currentD.makeDecision();
			if(selection==1){
				currentStats.updateStat(Skill.Intellect,1);
				return part4_sub2_doDay3m_visitForest();
			}
			else if(selection==2){
				currentStats.updateStat(Desire.Greed,1);
				return part4_sub2_doDay3m_visitForest();
			}
			else if(selection==3){
				currentStats.updateStat(Moral.Evil,2);
				return part7_StealGold(false,false);//Take gold without stranger knowing
			}
			else if(selection==4){
				currentStats.updateStat(Skill.SelfControl,2);
				currentStats.updateStat(Moral.Good,1);
				return 1;
			}
			return returnError();
		}
		private int part4_sub2_doDay3m_visitForest(){
			int selection=0;
			InOut.printLnSlow("You dig up the gold and examine it.  It is quite " +
			                      "heavy and is stamped with a serial number.  It looks genuine.");
			pause();
			currentD=new Decision("The gold would easily allow you to leave @village and do whatever you wanted.  Do you want to take the gold?");
			currentD.addOption(4,"Yes, I will take it.");
			currentD.addOption(1,"No, it wouldn't be the right thing to do.");
			currentD.addOption(3,"No, it is too dangerous.");
			if(currentStats.desireMoreThan(Desire.Greed,8)||!currentStats.skillCheck(Skill.SelfControl,4)){
				currentD.maybeAddOption(2,"No, it might not be worth enough to pay for your travel out of @village.",
				                        currentStats.desireMoreThan(Desire.Freedom,6)&&!currentStats.skillCheck(Skill.Intellect,6));
				
				selection=currentD.makeDecision();
				if(selection==1){
					InOut.printLnSlow("But you then realize that the stranger does not deserve the gold.  " +
					                  "He wants someone in the village killed!  You decide that it would not " +
					                  "be immoral to deprive him of gold that he doesn't deserve.  Therefore, you take the gold.");
					pause();
					return part7_StealGold(false,false);//Take gold without stranger knowing
				}
				else if(selection==2){
					currentStats.updateStat(Skill.Intellect,-2);
					InOut.printLnSlow("The value of the gold would easily pay for travel.  You decide to take it.");
					pause();
                    return part7_StealGold(false, false);//Take gold without stranger knowing
				}
				else if(selection==3){
					InOut.printLnSlow("You reluctantly replace the gold bar and return to your home.");
					pause();
					return 1;
				}
				else if(selection==4){
                    return part7_StealGold(false, false);//Take gold without stranger knowing
				}
			}
			else{
				selection=currentD.makeDecision();
				if(selection==4){
                    return part7_StealGold(false, false);//Take gold without stranger knowing
				}
				else if(selection==1){
					currentStats.updateStat(Skill.SelfControl,1);
					currentStats.updateStat(Desire.Greed,-1);
					return 1;
				}
				else if(selection==3){
					currentStats.updateStat(Skill.Intellect,1);
					InOut.printLnSlow("You reluctantly replace the gold bar and return to your home.");
					pause();
					return 1;
				}
			}
			return returnError();
		}
		private int part4_sub1_doDay3e(){
			InOut.printLnSlow("At the bar, the stranger tells a story about Leonardo da Vinci's " +
			                      "painting of the Last Supper.  He says that the same person was " +
			                      "used as a model for both Jesus and Judas.");
			InOut.printLnSlow("He concludes: \"So you see, Good and Evil have the same face; " +
			                 "it all depends on when they cross the path of each individual human being.\"");
			pause();
			InOut.printLnSlow("You feel awful that night and for the third night in a row cannot sleep.  " +
			                  "You hear a wolf howling and wonder if it is the rogue wolf, a wolf which " +
			                  "bit the blacksmith and has tasted human blood, making it more dangerous.");
			pause();
			return 1;
		}
		private int part4_sub1_doDay4m(){
			InOut.printLnSlow("You wake up and remember the stranger's " +
			                  "warning that if you fail to tell the village about his offer, " +
			                  "he will tell the villagers himself.");
			pause();
			return 1;
		}
		private int part4_sub1_doDay4e(){
			int selection=0;
			currentD=new Decision("That night, while collecting the money for the round of drinks " +
			                      "that the stranger usually bought, you notice that he has " +
			                      "slipped you a note.  What do you do?");
			currentD.addOption(1,"I place the note in my pocket and read it when I return to my room.");
			currentD.addOption(2,"I tear up the note (without reading it).");
			currentD.addOption(3,"I tell the villagers right then about the stranger's offer.");
			selection=currentD.makeDecision();
			if(selection==1){
				InOut.printLnSlow("The note note says that the stranger wants to meet with you tomorrow.");
				pause();
				InOut.printLnSlow("You finally sleep well that night.");
				pause();
				return 1;
			}
			else if(selection==2){
				InOut.printLnSlow("You finally sleep well that night.  You wonder what the message could have said.");
				pause();
				return 2;
			}
			else if(selection==3){
				return part5_tellVillageAboutOffer(4);
			}
			return returnError();
		}
		private int part4_sub1_doDay5m(bool knowsMeeting){
			int selection=0;
			if(knowsMeeting){
				currentD=new Decision("It is time for you to meet with the stranger.  What do you do?");
				currentD.addOption(1,"I go to meet with him.");
				currentD.addOption(2,"I ignore the stranger's note and do not meet with him.");
				selection=currentD.makeDecision();
				if(selection==1){
					currentD=new Decision("Do you take anything with you?");
					currentD.addOption(1,"Nothing.  I don't want him to be think I have bad intentions.");
					currentD.addOption(2,"I take my shotgun to intimidate him.");
					selection=currentD.makeDecision();
					if(selection==1){
                        currentStats.updateStat(Skill.PeopleSkills, 1);
						return part4_sub1_doDay5m_meetStranger(false);
					}
					else if(selection==2){
						return part4_sub1_doDay5m_meetStranger(true);
					}
				}
				else if(selection==2){
					return 1;
				}
			}
			else{
				currentD=new Decision("In the morning, the stranger indicates for you to come and talk to him.");
				currentD.addOption(1,"I follow him.");
				currentD.addOption(2,"I turn around and leave.");
				selection=currentD.makeDecision();
				if(selection==1){
					return part4_sub1_doDay5m_meetStranger(false);
				}
				else if(selection==2){
					return 1;
				}
			}
			return returnError();
		}
		private int part4_sub1_doDay5m_meetStranger(bool withGun){
			int selection=0;
			if(withGun){
				currentStats.updateStat(Skill.Agility,1);
				currentD=new Decision("You meet the stranger.");
				currentD.addLine("He says: \"You've got a shotgun in there.  Are you going to kill me?\"");
				currentD.addOption(1,"\"No, only if you don't cooperate.\"");
				currentD.addOption(2,"\"No, I have it in case the rogue wolf attacks.\"");
				currentD.addOption(3,"\"Yes!\"");
				selection=currentD.makeDecision();
				if(selection==1||selection==3){
					if(selection==1){
						currentStats.updateStat(Skill.PeopleSkills,2);
					}
					currentD=new Decision("\"That's all right, because you killing me provides me with " +
					                      "an answer to my question: human beings are essentially evil.  " +
					                      "I'm going to leave but now I have my answer, " +
					                      "so I can die happy.\"");
					currentD.addOption(1,"(you shoot him.)");
					currentD.addOption(2,"\"What do you want?  Why did you give me that note?\"");
					selection=currentD.makeDecision();
					if(selection==1){
                        return part7_ShootHim();//Shoots him
					}
					else if(selection==2){
						return part4_sub2_doDay5m_meetStrangerAndTalk();//discussion
					}
					
				}
				else if(selection==2){
					currentD=new Decision("(The stranger looks unconvinced.)");
					currentD.addOption(1,"(you shoot him.)");
					currentD.addOption(2,"\"What do you want?  Why did you give me that note?\"");
					selection=currentD.makeDecision();
					if(selection==1){
                        return part7_ShootHim();//Shoots him
					}
					else if(selection==2){
						return part4_sub2_doDay5m_meetStrangerAndTalk();//discussion
					}
				}
				
			}
			else{
				return part4_sub2_doDay5m_meetStrangerAndTalk();//discussion
			}
			return returnError();
		}
		private int part4_sub2_doDay5m_meetStrangerAndTalk(){
			int selection=0;
			currentD=new Decision("\"I understand why you're delaying, but I can't wait any longer.\"");
			currentD.addOption(1,"\"I'm going to tell the village about your offer this evening.\"");
			currentD.addOption(2,"\"You know, I could just steal the gold and you would never see me again.\"");
			currentD.addOption(3,"\"Why do you want me to do this?\"");
			currentD.addOption(4,"\"I will consider telling the village about your offer tonight.\"");
			selection=currentD.makeDecision();
			if(selection==1){
				currentD=new Decision("\"If not, I will be forced to tell them myself.\"");
				currentD.addOption(1,"\"Why do you want them to take the gold?  What is the point of this?\"");
				currentD.addOption(2,"(You finish the meeting and leave.)");
				selection=currentD.makeDecision();
				if(selection==1){
					return part4_sub3_doDay5m_StrangerBG(); //More info on life
				}
				else if(selection==2){
					return 1;
				}
			}
			else if(selection==2){
				currentD=new Decision("(stranger smiles faintly.) \"At times, I have worked for the secret service.\"");
				currentD.addOption(1,"\"Why do you want me to take the gold?  What is the point of this?\"");
				currentD.addOption(2,"\"I will take that risk.\" (You take the gold.)");
				currentD.addOption(3,"\"What did you do before before you visited @village?\"");
				selection=currentD.makeDecision();
				if(selection==1||selection==3){
					return part4_sub3_doDay5m_StrangerBG(); //More info on life
				}
				else if(selection==2){
					return part7_StealGold(true,false); //Steal gold (with stranger watching)
				}
			}
			else if(selection==3){
				return part4_sub3_doDay5m_StrangerBG(); //More info on life
			}
			else if(selection==4){
				InOut.printLnSlow("\"If not, I will be forced to tell them myself.\"  (Your meeting is done.  You leave.)");
				pause();
				return 1;
			}
			return returnError();
		}
		private int part4_sub3_doDay5m_StrangerBG(){
			InOut.printLnSlow("\"Originally, I worked as arms manufacturer.  My weapons were made to help defend order, " +
			                  "which is the only way to ensure progress and development in this world, or so I thought.  " +
			                  "I personally checked all our transactions and uncovered several cases of corruption.  " +
			                  "I considered myself what people usually term a \"good man\".  Then one evening I received a " +
			                  "phone call stating that my family had been kidnapped by terrorists and I had to give them " +
			                  "weapons in order for them to be released.  Since I was a good citizen, I called the police.  " +
			                  "Before the day was out, the hiding place had been discovered, and the kidnappers lay dead.  " +
			                  "Before they died, however, they had time to execute my wife and children.  Both the police " +
			                  "and the kidnappers used weapons made by my company.  This is the reason for my experiment.  " +
			                  "I want to know what was going on in the minds of those terrorists. I want to know whether, " +
			                  "at any point, they might have taken pity on them and just let them leave, because their war " +
			                  "had nothing to do with my family. I want to know if, when Good and Evil are with my family " +
			                  "struggling against each other, there is a fraction of a second when Good might prevail.\"",5);
			pause();
			InOut.printLnSlow("(Your meeting is done.  You leave.)");
			pause();
			return 1;
		}
		private int part4_sub1_doDay5e(){
			int selection=0;
			currentD=new Decision("That evening, you are concerned that this is your last chance to tell the villagers about the stranger's offer.");
			currentD.addOption(1,"I tell the villagers right now.");
			currentD.addOption(2,"I do not tell the villagers.");
			selection=currentD.makeDecision();
			if(selection==1){
				return part5_tellVillageAboutOffer(5);
			}
			else if(selection==2){
				InOut.printLnSlow("You sleep restlessly.  You worry about what the stranger will do next.");
				pause();
				return 1;
			}
			return returnError();
		}
		private int part4_sub1_doDay6m(){
			InOut.printLnSlow("You notice the stranger talking to various members of the village.  You do not hear what he tells them.");
			pause();
			return 1;
		}
		private int part4_sub1_doDay6e(){
			InOut.printLnSlow("The bar is very quite that night and few people are there.");
			pause();
			//currentStats.addScoringItem(-1000, "You were killed: ");
            currentStats.setScoreingItems(false, true, false, false, 0, false,false);
			endGame("Suddenly, a group of villagers enter the bar and use their rifles to kill you.");
			return 3;
		}
		private int part5_tellVillageAboutOffer(int day){
			int selection=0;
			currentD=new Decision("You decide to tell the villagers that evening at the bar.  What do you tell them?");
			currentD.maybeAddOption(1,"You tell the villagers about the stranger's offer and how it disgusts you.",
			                        currentStats.desireLessThan(Desire.Greed,6));
			currentD.addOption(2,"I encourage the village to accept the offer and suggest meeting about it tomorrow morning.");
			currentD.addOption(3,"I simply explain the offer.");
			selection=currentD.makeDecision();
			if(selection==1){
                currentStats.updateStat(Moral.Good, 2);
				currentD=new Decision("The crowd is stunned by what the stranger wants them to do.  What do you do next?");
				currentD.addOption(1,"I tell them that I have confidence that no one will be killed.");
				currentD.addOption(2,"I tell the crowd that I will tell the police if anyone is murdered and the murderer will be arrested.");
                currentD.maybeAddOption(3, "I call the police to report the stranger as a suspicious individual (while the crowd is watching).", 
                    !currentStats.getEvents().reportedToPolice());
				currentD.addOption(4,"I call the police to press charges against the stranger and arrest him (while the crowd is watching).");
                currentD.maybeAddOption(5, "I tell the crowd that I have already reported the stranger as a suspicious person " +
                    "and they will investigate if anything were to happen.", currentStats.getEvents().reportedToPolice());
				selection=currentD.makeDecision();
				if(selection==1){
					return part6_killBertaOrYou(day);//They may go after you or Berta
				}
				else if(selection==2){
                    currentStats.updateStat(Moral.Good, 2);
					return part5_sub1_theyKillYou(day);
					
				}
				else if(selection==3){
                    currentStats.updateStat(Moral.Good, 1);
					InOut.printLnSlow("You report the stranger to the police.  " +
					                  "They thank you for the report, but decide not to investigate unless an incident occurs.");
					pause();
					return part5_sub2_theyAreTooScaredToKill(day);//They are afraid and kill no one
				}
				else if(selection==4){
					return part5_sub3_tryToArrestStranger(day);//The stranger tries to convince them not to arrest him
				}
                else if (selection == 5)
                {
                    return part5_sub2_theyAreTooScaredToKill(day);//They are afraid and kill no one
                }
			}
			else if(selection==2){
                currentStats.updateStat(Moral.Evil, 2);
                return part7_YouTellThemWhoToKill(day);//You help them pick a target
			}
			else if(selection==3){
                return part6_killBertaOrYou(day);//They may go after you or Berta
			}
			return returnError();
		}
		private int part5_sub1_theyKillYou(int day){
			int nextDay=day+1;
			InOut.printFullScreen("DAY "+nextDay);
			InOut.printLnSlow("The village is very quiet today.  Few people come to the bar.");
			pause();
			//currentStats.addScoringItem(-1000, "You were killed: ");
            currentStats.setScoreingItems(false, true, false, false, 0, false,false);
			endGame("Suddenly, a group of villagers enter the bar and use their rifles to kill you.  You can't report them to the police if you are dead.");
			return 3;
		}
		private int part5_sub2_theyAreTooScaredToKill(int day){
			int selection=0;
			int nextDay=day+1;
			InOut.printFullScreen("DAY "+nextDay);
			currentD=new Decision("No one talks about the events of the pervious day.  " +
			                      "A man who travels from village to village selling bread asks a group of villagers if anything is wrong.");
			currentD.addOption(1,"I see how the villagers respond.");
			currentD.addOption(2,"I interrupt and tell them that a man has arrived in " +
			                   "the village with 10 bars of gold and will give them away if someone commits a murder.");
			selection=currentD.makeDecision();
			if(selection==1){
				InOut.printLnSlow("The blacksmith claims that nothing is happening.");
				pause();
				return part5_sub4_slowlyChangeMind(nextDay); //Slowly change mind
			}
			else if(selection==2){
				InOut.printLnSlow("The blacksmith tells the man selling the bread that the stranger " +
				                  "wasn't really offering gold, it was just a story he told the villagers to entertain them.");
				pause();
				//currentStats.addScoringItem(500,"No one was killed: ");
				//currentStats.addScoringItem(0,"You did not get any gold: ");
                currentStats.setScoreingItems(false, false, false, false, 0, false,false);
				endGame("After 7 days, no one dares to commit a murder.  The villager of @village are surprised " +
				        "that you were going to tell the other villages about the stranger's offer and because " +
				        "you already alerted the police, they are worried that if they kill you the police will investigate.  " +
				        "The stranger leaves at the end of the week.");
				return 3;
			}
			return returnError();
		}
		private int part5_sub3_tryToArrestStranger(int day){
			int selection=0;
			currentD=new Decision("Before you call the police, the stranger " +
			                      "says you have no evidence against him and he " +
			                      "could convince the police not to arrest him.");
			currentD.addOption(1,"I call the police.");
			currentD.addOption(2,"I do not call the police.");
			selection=currentD.makeDecision();
			if(selection==1){
                currentStats.updateStat(Moral.Good, 1);
				currentD=new Decision("When the police arrive, you tell them about what the stranger said.  " +
					                   "Do you tell them about the one gold bar that the stranger offered you?");
				currentD.addOption(1,"Yes, I present the bar as evidence that the story is true.");
				currentD.addOption(2,"No, I leave out that detail.");
				selection=currentD.makeDecision();
				if(selection==1){
					//currentStats.addScoringItem(200,"The stranger is arrested: ");
					//currentStats.addScoringItem(500,"No one was killed: ");
					//currentStats.addScoringItem(0,"You did not get any gold: ");
                    currentStats.updateStat(Moral.Good, 1);
                    currentStats.setScoreingItems(false, false, false, false, 0, false, true);
                    endGame("The stranger is arrested.  Order is restored.");
                    return 3;
				}
				else if(selection==2){
                    currentStats.updateStat(Moral.Evil, 1);
					if(currentStats.skillCheck(Skill.PeopleSkills,6)){
						//currentStats.addScoringItem(200,"The stranger is arrested: ");
						//currentStats.addScoringItem(500,"No one was killed: ");
						currentD=new Decision("The villagers are questioned and their stories match.  " +
						                      "The stranger is arrested.  What do you do next?");
						currentD.addOption(1,"I continue to live at @village.");
						currentD.addOption(2,"I take the bar of gold that the stranger has left behind.");
						selection=currentD.makeDecision();
						if(selection==1){
							currentStats.updateStat(Moral.Good,3);
                            currentStats.setScoreingItems(false, false, false, false, 0, false, true);
                            endGame("The stranger is found guilty and arrested.  Order is restored, but you remain at @village.");
                            return 3;
                        }
						else if(selection==2){
							currentStats.updateStat(Moral.Evil,2);
                            //currentStats.addScoringItem(100, "You received one bar of gold,");
                            currentStats.setScoreingItems(false, false, false, false, 1, true, true);
							endGame("You escape @village with your bar of gold.  The villagers " +
							        "wonder about your disappearance but cannot do anything about it.");
                            return 3;
						}
					}
					else{
						InOut.printLnSlow("The villagers are questioned but are afraid of the stranger " +
						                  "so they do not report the events that occurred and their stories do not " +
						                  "match.  The police find insufficient evidence.");
						pause();
						return part5_sub2_theyAreTooScaredToKill(day);//Slowly change mind
					}
				}
			}
			else if(selection==2){
                return part6_killBertaOrYou(day);//May kill one or other
			}
			return returnError();
		}
		private int part5_sub4_slowlyChangeMind(int day){
			int currentDay=day;
            int result = 0;
			InOut.printLnSlow("At the bar, the stranger tells a story about a police officer who " +
			                  "was bribed to keep quiet about a crime.  He concludes that with enough money, one can get away with anything.");
			pause();
            result = part4_DayDecision(day,true);//offer choice to steal gold.
            if (result == 3) { return result; }
			if(currentDay>=7){
				return part5_sub5_timeIsUpYouLive(currentDay);//You do not die
			}
			currentDay++;
			InOut.printFullScreen("DAY "+currentDay);
			InOut.printLnSlow("The villagers avoid talking to you today.  " +
			                  "It is as if they blame you for putting " +
			                  "the burden of the stranger's offer before them.");
			pause();
            result = part4_DayDecision(day, true);//offer choice to steal gold.
            if (result == 3) { return result; }
			if(currentDay>=7){
				return part5_sub5_timeIsUpYouLive(currentDay);//You do not die
			}
			currentDay++;
			InOut.printFullScreen("DAY "+currentDay);
			InOut.printLnSlow("The village is very quiet today.  Few people came to the bar.");
			pause();
			//currentStats.addScoringItem(-1000, "You were killed: ");
            currentStats.setScoreingItems(false, true, false, false, 0, false, false);
			endGame("Suddenly, a group of villagers enter the bar and use their rifles to kill you.");
			return 3;
		}
		private int part5_sub5_timeIsUpYouLive(int day){
			//currentStats.addScoringItem(500,"No one was killed: ");
			//currentStats.addScoringItem(0,"You did not get any gold: ");
            currentStats.setScoreingItems(false, false, false, false, 0, false, false);
			endGame("After 7 days, no one has dared to commit a murder because they are " +
			        "afraid it will attract the attention of the police.  The stranger leaves.");
			return 3;
		}
		private int part6_killBertaOrYou(int day){
			int selection=0;
			int nextDay=day;
			nextDay++;
			InOut.printFullScreen("DAY "+nextDay);
			currentD=new Decision("No one talks about the events of the pervious day.  " +
			                      "A man who travels from village to village selling bread " +
			                      "asks a group of villagers if anything is wrong.");
			currentD.addOption(1,"I see how the villagers respond.");
			currentD.addOption(2,"I interrupt and tell them that a man has arrived in " +
			                   "the village with 10 bars of gold and will give them away " +
			                   "if someone commits a murder.");
			selection=currentD.makeDecision();
			if(selection==1){
				currentD=new Decision("The blacksmith claims that nothing is happening.");
				currentD.addOption(1,"I confront him and ask why he said that.");
				currentD.addOption(2,"I tell him that if the village tries to kill anyone, I will report them to the police.");
				currentD.addOption(3,"I do not respond.");
				selection=currentD.makeDecision();
				if(selection==1){
					currentD=new Decision("He says: \"You know what is going on.  You want us to commit a murder in return for money.\"");
					currentD.addOption(1,"No I don't!");
					currentD.addOption(2,"Of course!  You don't want @village to collapse, do you?");
					selection=currentD.makeDecision();
					InOut.printLnSlow("The hotel landlady interrupts your conversation.");
					InOut.printLnSlow("She says: \"Let's just go home and have breakfast.\"");
					pause();
					if(selection==1){
						return part6_sub1_KillBerta(nextDay);//Berta
					}
					else if(selection==2){
                        currentStats.updateStat(Moral.Evil, 1);
                        InOut.printLnSlow("The priest holds a meeting to discuss what to do about the stranger's offer.  You attend.");
                        pause();
                        return part7_sub1_PriestInCharge();//You in control
					}
				}
				else if(selection==2){
                    currentStats.updateStat(Moral.Good, 1);
                    return part6_sub1_KillYou(nextDay); //kill you
				}
				else if(selection==3){
                    return part6_sub1_KillBerta(nextDay); //Berta
				}
				
			}
			else if(selection==2){
				InOut.printLnSlow("The blacksmith tells the man selling the bread that the stranger " +
				                  "wasn't really offering gold, it was just a story he told the villagers to entertain them.");
				pause();
                return part6_sub1_KillYou(nextDay);//Kill you
			}
            return returnError();
		}
		private int part6_sub1_KillBerta(int day){
            int selection = 0;
			int currentDay=day;
			InOut.printLnSlow("In the evening, almost everyone except you is at the church.  They discuss how to respond to the stranger's offer.");
			pause();
			currentDay++;
			InOut.printFullScreen("DAY "+currentDay);
            InOut.printLnSlow("The landlady tells you that all the men are meeting in the square this morning.  You are worried they are planning something.");
            pause();
            //Opportunity to steal gold
            currentD = new Decision("That evening, you hear a loud crowd of people in the street.  What do you do?");
            currentD.addOption(1,"I follow them to see what is going on.");
            currentD.addOption(2,"I stay out of their way.");
            selection = currentD.makeDecision();
            if (selection == 1)
            {
                currentD = new Decision("They are carrying Berta who is unconscious.  They plan on killing her.");
                currentD.addOption(1,"I try to stop them.");
                currentD.addOption(2, "I do nothing.");
                selection = currentD.makeDecision();
                if (selection == 1)
                {
                    currentD = new Decision("What do you say?");
                    currentD.addOption(1, "\"Stop!  Can't you see how immoral this is?\"");
                    currentD.addOption(2, "\"Wait! Have you seen the gold yet?\"");
                    selection = currentD.makeDecision();
                    if (selection == 1)
                    {
                        currentStats.updateStat(Moral.Good, 1);
                        InOut.printLnSlow("The priest says: \"Sometimes the innocent must be sacrificed for the good of the community.\"");
                        pause();
                        //currentStats.addScoringItem(200, "The village got 10 bars of gold: ");
                        //currentStats.addScoringItem(-500, "Berta was killed: ");
                        currentStats.setScoreingItems(false, false, true, false, 2, false, false);
                        endGame("BANG!  The crowd kills Berta to earn the gold.  The stranger gives the village the 10 bars.");
                        return 3;
                    }
                    else if (selection == 2)
                    {
                        currentD = new Decision("This gets their attention.");
                        currentD.addOption(1, "\"The gold is useless to you!  You cannot cash it without the bank becoming suspicious.\"");
                        currentD.addOption(2, "\"The gold is most likely fake!  Why would the stranger come to @village if he had 10 bars of real gold?\"");
                        selection=currentD.makeDecision();
                        if (selection == 1)
                        {
                            currentD = new Decision("The landowner responds: \"We will have the mayor cash it.  They won't suspect him.\"");
                            currentD.addOption(1, "\"They will not trust him.  He is not a figure of authority.\"");
                            currentD.addOption(2, "They will want the purchase documents.");
                            selection = currentD.makeDecision();
                            if (selection == 1)
                            {
                                InOut.printLnSlow("\"Yes he is.  Now let's finish this!\"");
                                pause();
                                //currentStats.addScoringItem(200, "The village got 10 bars of gold: ");
                                //currentStats.addScoringItem(-500, "Berta was killed: ");
                                currentStats.setScoreingItems(false, false, true, false, 2, false, false);
                                endGame("BANG!  The crowd kills Berta.  The stranger gives the village the 10 bars.");
                                return 3;
                            }
                            else if (selection == 2)
                            {
                                InOut.printLnSlow("The crowd is now less confident.  One person disarmed his shotgun.  The rest followed.  "+
                                                  "They slowly walked back down the hillside.");
                                pause();
                                //currentStats.addScoringItem(500, "You got 11 bars of gold: ");
                                //currentStats.addScoringItem(500, "No one was killed: ");
                                currentStats.updateStat(Moral.Good, 5);
                                currentStats.setScoreingItems(false, false, false, false, 3, true, false);
                                endGame("The stranger decides to give you the 11 bars of gold as a reward for your strong morals.");
                                return 3;
                            }
                        }
                        else if (selection == 2)
                        {
                            InOut.printLnSlow("They check the gold and it is real.");
                            pause();
                            //currentStats.addScoringItem(200, "The village got 10 bars of gold: ");
                            //currentStats.addScoringItem(-500, "Berta was killed: ");
                            currentStats.setScoreingItems(false,false,true,false,2,false,false);
                            endGame("BANG!  The crowd kills Berta.  The stranger gives the village the 10 bars.");
                            return 3;
                        }
                    }
                }
                else if (selection == 2)
                {
                    //currentStats.addScoringItem(200, "The village got 10 bars of gold: ");
                    //currentStats.addScoringItem(-500, "Berta was killed: ");
                    currentStats.setScoreingItems(false,false,true,false,2,false,false);
                    endGame("The crowd kills Berta to earn the gold.  The stranger gives the village the 10 bars and leaves at the end of the week.");
                    return 3;
                }
            }
            else if (selection == 2)
            {
                //currentStats.addScoringItem(200, "The village got 10 bars of gold: ");
                //currentStats.addScoringItem(-500, "Berta was killed: ");
                currentStats.setScoreingItems(false,false,true,false,2,false,false);
                endGame("You later discover that the crowd killed Berta to earn the gold.  The stranger gives the village the 10 bars and leaves at the end of the week.");
                return 3;
            }
            return returnError();
		}
        private int part6_sub1_KillYou(int day)
        {
            int currentDay = day;
            InOut.printLnSlow("In the evening, almost everyone except you is at the church.  They discuss how to respond to the stranger's offer.");
            pause();
            currentDay++;
            InOut.printFullScreen("DAY " + currentDay);
            InOut.printLnSlow("The landlady tells you that all the men are meeting in the square this morning.  You are worried they are planning something.");
            pause();
            //Opportunity to steal gold
            //currentStats.addScoringItem(-1000, "You were killed: ");
            currentStats.setScoreingItems(false, true, false, false, 0, false, false);
            endGame("Suddenly, a group of villagers enter the bar and use their rifles to kill you.");
            return 3;
        }
        private int part7_StealGold(bool knows, bool killed)
        {
            int selection = 0;
            currentStats.updateStat(Moral.Evil, 2);
            currentD = new Decision("You take the gold.  What do you do next?");
            currentD.addOption(1,"I hide it in my house.");
            currentD.addOption(2,"I flee from @village.");
            currentD.addOption(3,"I add it to my bank account but stay in @village.");
            selection = currentD.makeDecision();
            if (selection != 2 && !killed)
            {
                //currentStats.addScoringItem(-500, "You were arrested: ");
                currentStats.setScoreingItems(false, false, false, true, 1, false, false);
                endGame("The stranger knows you took the gold and has you arrested.");
            }
            else if (selection != 2 && killed)
            {
                //currentStats.addScoringItem(-500, "You were arrested: ");
                currentStats.setScoreingItems(false, false, false, true, 1, false, false);
                endGame("Some police come looking for the stranger and discover that he was murdered.  They search your house and find the gold.  You are arrested.");
            }
            else if (selection == 2 && !killed)
            {
                //currentStats.addScoringItem(-500, "You were arrested: ");
                currentStats.setScoreingItems(false, false, false, true, 1, false, false);
                endGame("The stranger uses his connections to the secret service to track you down and has you arrested.");
            }
            else if (selection == 2 && killed)
            {
                currentStats.updateStat(Moral.Evil, 5);
                currentStats.setScoreingItems(false, false, false, false, 1, true, false);
                endGame("You escape @village successfully.  The police investigate the stranger's murder but you are long gone.");
            }
            return 3;
        }
        private int part7_ShootHim()
        {
            int selection = 0;
            currentStats.updateStat(Moral.Evil, 1);
            currentStats.updateStat(Skill.PeopleSkills, -1);
            currentD = new Decision("What do you do now?");
            currentD.addOption(1, "Wait at @village.");
            currentD.addOption(2, "Take the gold.");
            selection = currentD.makeDecision();
            if (selection == 1)
            {
                //currentStats.addScoringItem(-500, "You were arrested: ");
                currentStats.setScoreingItems(false, false, false, true, 0, false, false);
                endGame("At the end of the week police come to investigate.  After some questioning, you are arrested.");
                return 3;
            }
            else if (selection == 2)
            {
                return part7_StealGold(false, true);
            }
            return returnError();
        }
        private int part7_ReportToPolice()
        {
            currentStats.updateStat(Moral.Good, 1);
            InOut.printLnSlow("You tell the police about the stranger and they thank you for the report but do not investigate due to insufficient evidence.");
            currentStats.getEvents().reportToPolice();
            pause();
            return 1;
        }
        private int part7_YouTellThemWhoToKill(int day)
        {
            int result = 0;
            int selection=0;
            int currentDay = day;
            InOut.printLnSlow("The crowd agrees to meet with you tomorrow to discuss how to respond.");
            pause();
            currentDay++;
            InOut.printFullScreen("DAY " + currentDay);
            result = part4_DayDecision(day, true);//offer choice to steal gold.
            if (result == 3) { return result; }
            currentD = new Decision("It is time for your meeting.  You have decided to meet in the square.  "+
                "Almost everyone from the village attends.  Only Berta stays at her house.  What do you do next?");
            currentD.addOption(1, "I take charge of the meeting.");
            currentD.addOption(2, "I let the priest take charge so I am not at risk if the crowd dislikes my ideas.");
            currentD.addOption(3, "I let the mayor take charge because he has the most authority.");
            selection = currentD.makeDecision();
            if (selection == 1)
            {
                currentD=new Decision("Before you begin, the priest tries to get everyone's attention.");
                currentD.addOption(1, "I let him speak.");
                currentD.addOption(2, "I tell the villagers to listen to me.");
                selection = currentD.makeDecision();
                if (selection == 1)
                {
                    return part7_sub1_PriestInCharge();
                }
                else if (selection == 2)
                {
                    InOut.printLnSlow("The villagers do not like taking orders from a bar tender.  "+
                        "They listen to the priest.  He determines that they should kill Berta.  You are not invited.");
                    pause();
                    return part7_sub3_TheyDontTrustYou(false);
                }
            }
            else if (selection == 2)
            {
                return part7_sub1_PriestInCharge();
            }
            else if (selection == 3)
            {
                InOut.printLnSlow("The mayor talks with the priest.  He allows the priest to address the people.");
                return part7_sub1_PriestInCharge();
            }
            return returnError();
            
        }
        private int part7_sub1_PriestInCharge()
        {
            int selection = 0;
            InOut.printLnSlow("The priest says: \"Only by sacrifice and penitence can we find salvation.  " +
                    "I mean the sacrifice of one Person, the penitence of all and the salvation of this village.\"");
            pause();
            currentD = new Decision("The priest discusses the details of how the crime will be carried out.  He asks who the victim should be.");
            currentD.addOption(1, "I do not voice an opinion to avoid making enemies");
            currentD.addOption(2, "I suggest the hotel landlady because she pays me too little");
            currentD.addOption(3, "I suggest the landowner because he would profit if @village was sold to a large company");
            currentD.addOption(4, "I suggest Berta because she is old and will die soon anyway.");
            selection = currentD.makeDecision();
            if (selection == 1)
            {
                InOut.printLnSlow("The crowd decides to kill Berta because she does not contribute to the village and will die soon anyway.  " +
                    "They will carry out the plan tonight.");
                pause();
                return part7_sub2_HelpKillOrNot();
            }
            else if (selection == 2)
            {
                //currentStats.addScoringItem(-200, "You were fired: ");
                InOut.printLnSlow("The landlady is present at the meeting.  She is furious and fires you.  The village decides to kill Berta because she does not "+
                    "contribute to the village and will die soon anyway.  They will carry out the plan tonight.  You are not invited.");
                pause();
                return part7_sub3_TheyDontTrustYou(false);
            }
            else if (selection == 3)
            {
                //currentStats.addScoringItem(-200, "You were fired: ");
                InOut.printLnSlow("The landowner is present at the meeting.  He is furious and convinces the hotel landlady to fire you.  The village decides to kill Berta because she does not " +
                    "contribute to the village and will die soon anyway.  They will carry out the plan tonight.  You are not invited.");
                pause();
                return part7_sub3_TheyDontTrustYou(false);
            }
            else if (selection == 4)
            {
                InOut.printLnSlow("The crowd agrees with you.  " +
                    "They will carry out the plan tonight.");
                pause();
                return part7_sub2_HelpKillOrNot();
            }
            return returnError();
        }
        private int part7_sub2_HelpKillOrNot()
        {
            int selection = 0;
            currentD = new Decision("It is now the time the crowd selected to kill Berta.  What do you do?");
            currentD.addOption(1,"I help with the murder so the crowd will see that I am loyal to them.");
            currentD.addOption(2, "I do not attend the murder so I am not involved.");
            currentD.addOption(3, "I steal the gold and run.");
            selection = currentD.makeDecision();
            if (selection == 1)
            {
                currentStats.updateStat(Moral.Evil, 2);
                InOut.printLnSlow("A group of men enter carrying Berta who is unconscious.  The mayor tells you to aim.");
                pause();
                currentD = new Decision("He says: \"Fire!\"");
                currentD.addOption(1, "I fire.");
                currentD.addOption(2, "I wait for the others to fire first.");
                selection = currentD.makeDecision();
                if (selection == 1)
                {
                    //currentStats.addScoringItem(-500, "Berta was killed: ");
                    //currentStats.addScoringItem(200, "The village gets 10 bars of gold: ");
                    currentStats.updateStat(Moral.Evil, 10);
                    currentStats.setScoreingItems(false, false, true, false, 2, false, false);
                    endGame("The sound of gunshots shatters the silence.  Berta is dead.  The village receives its gold.");
                    return 3;
                }
                else if (selection == 2)
                {
                    InOut.printLnSlow("The villagers pause, waiting for someone to shoot first.  They look at each other, "+
                        "but no one dares to fire the first shot.  Someone disarms his gun.  The rest follow.");
                    pause();
                    currentStats.setScoreingItems(false, false, false, false, 0, false, false);
                    endGame("The stranger leaves at the end of the week and things eventually return to normal.");
                    return 3;
                }
            }
            else if (selection == 2)
            {
                return part7_sub3_TheyDontTrustYou(true);
            }
            else if (selection == 3)
            {
                return part7_StealGold(false, false);
            }
            return returnError();
        }
        private int part7_sub3_TheyDontTrustYou(bool invited)
        {
            InOut.printLnSlow("You hear the people in the square preparing to kill Berta.");
            pause();
            string reason = "They no longer trust you and fear that you might tell others about the incident.  ";
            if (invited)
            {
                reason = "Because you did not show up, they no longer trust you and fear that you might tell others about the incident.  ";
            }
            //currentStats.addScoringItem(-1000, "You were killed: ");
            currentStats.setScoreingItems(false, true, false, false, 0, false, false);
            endGame("Except it is not Berta they have decided to kill.  " + reason + "They have selected you as their target instead of Berta.  They surround you.  BANG!");
            return 3;
        }
        private int part7_TalkToMayor(bool tooLate)
        {
            currentStats.getEvents().talkToFriend();
            int selection = 0;
            currentD = new Decision("You meet with the Mayor.  What do you tell him?");
            currentD.maybeAddOption(1,"I tell him about the stranger and his offer.",!tooLate);
            currentD.addOption(2,"I tell him to be wary of the stranger because he is suspicious.");
            selection = currentD.makeDecision();
            if (selection == 1)
            {
                InOut.printLnSlow("He looks very surprised and concerned.  He tells you not to tell anyone until he has decided how to respond.");
                pause();
                if (currentStats.getEvents().reportedToPolice())
                {
                    currentD = new Decision("Do you tell him that you reported the stranger to the police?");
                    currentD.addOption(1, "Yes.  He should know what measures I have taken.");
                    currentD.addOption(2, "No.  He might be upset that I took action before talking to him.");
                    selection = currentD.makeDecision();
                    if (selection == 1)
                    {
                        InOut.printLnSlow("He thanks you for the information.");
                        pause();
                        if (!tooLate)
                        {
                            currentStats.setScoreingItems(false, false, false, false, 0, false, false);
                            endGame("The mayor would like to save @village by killing someone but is afraid to " +
                                "because of your report to the police.  At the end of the week the stranger leaves.  No one is killed.");
                            return 3;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else if (selection == 2)
                    {
                        InOut.printLnSlow("He thanks you for the information.");
                        pause();
                        return 1;
                    }
                }
                else
                {
                    return 1;
                }
            }
            else if (selection == 2)
            {
                InOut.printLnSlow("He does not agree but says he will look out for anything out of the ordinary.");
                pause();
                return 1;
            }
            return returnError();
        }
        private int part7_TalkToPriest(bool tooLate)
        {
            currentStats.getEvents().talkToFriend();
            int selection = 0;
            currentD = new Decision("You meet with the Priest.  What do you tell him?");
            currentD.maybeAddOption(1, "I tell him about the stranger and his offer.",!tooLate);
            currentD.addOption(2, "I tell him to be wary of the stranger because he is suspicious.");
            currentD.addOption(3, "I ask him for advice on how to handle a dilemma.");
            selection = currentD.makeDecision();
            if (selection == 1)
            {
                InOut.printLnSlow("He looks very surprised and concerned.  He tells you not to tell anyone until he has decided how to respond.");
                pause();
                if (currentStats.getEvents().reportedToPolice())
                {
                    currentD = new Decision("Do you tell him that you reported the stranger to the police?");
                    currentD.addOption(1, "Yes.  He should know what measures I have taken.");
                    currentD.addOption(2, "No.  He might be upset that I took action before talking to him.");
                    selection = currentD.makeDecision();
                    if (selection == 1)
                    {
                        InOut.printLnSlow("He thanks you for the information.");
                        pause();
                        if (!tooLate)
                        {
                            currentStats.setScoreingItems(false, false, false, false, 0, false, false);
                            endGame("The priest would like to save @village by killing someone but is afraid to " +
                                "because of your report to the police.  At the end of the week the stranger leaves.  No one is killed.");
                            return 3;
                        }
                        else
                        {
                            return 1;
                        }
                    }
                    else if (selection == 2)
                    {
                        InOut.printLnSlow("He thanks you for the information.");
                        pause();
                        return 1;
                    }
                }
                else
                {
                    return 1;
                }
            }
            else if (selection == 2)
            {
                InOut.printLnSlow("He does not agree but says he will look out for anything out of the ordinary.");
                pause();
                return 1;
            }
            else if (selection == 3)
            {
                InOut.printLnSlow("He says: \"do what your heart commands, and God will be happy.\"");
                pause();
                InOut.printLnSlow("You thank him and leave.");
                return 1;
            }
            return returnError();
        }
        private int part7_TalkToBerta()
        {
            currentStats.getEvents().talkToFriend();
            int selection = 0;
            currentD = new Decision("You visit Berta's house.  What do you say?");
            currentD.addOption(1, "I tell her I am trying to make a difficult decision and need advice.");
            currentD.addOption(2, "I tell her that the stranger concerns me and I am not sure what to do.");
            selection = currentD.makeDecision();
            if (selection == 1)
            {
                InOut.printLnSlow("She tells you a story about heaven and hell.  In it, a man traveling "+
                    "with his dog refuses to enter heaven because the guard says that pets are not allowed.  "+
                    "He later discovers that he had actually passed by hell, not heaven.  Berta concludes you should never abandon your friends.");
                pause();
                InOut.printLnSlow("You thank her and return home.");
                return 1;
            }
            else if (selection == 2)
            {
                InOut.printLnSlow("She agrees that the stranger may be planning something hostile and advises you to leave the village for a time.  "+
                    "You do not tell her that you do not have enough money to go without working for a week.  But if you took the gold...");
                pause();
                return 1;
            }
            return returnError();
        }

		private void pause(){
			InOut.pause(pauseMessage);
		}
		private int returnError()
		{
			//InOut.printLn("Oh no! An error occurred!");
			//pause();
			return -1;
		}
		private int testRun(int result)
		{
			return testRun(result, "Code complete.");
		}
		private int testRun(int result, string message)
		{
			InOut.printLn(message + " " + "Result: " + result);
			pause();
			return result;
		}
		private int notImplemented(int test)
		{
			InOut.printLn("Sorry, that is not yet implemented.");
			pause();
			return -1;
		}
		private bool getRand(int prob){
			return prob>rand.Next(0,100);
		}
		private void endGame(string message){
			InOut.printLnSlow(message);
			pause();
			currentStats.printEndStats();
		}

	}
}
//#change periods
//#change morals and skills
//#change full lines have extra returns
//#change tense to present
//#change 1st, 2nd person
//#change days in slowly change mind need options.
//#change check endgames return 3.