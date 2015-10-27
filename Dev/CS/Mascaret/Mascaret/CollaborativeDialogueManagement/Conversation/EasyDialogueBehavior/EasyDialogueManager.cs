using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
namespace NLP
{

    public class Dialogue
    {
        //private Dictionary<int, DialogueLine> lines = new Dictionary<int, DialogueLine>();
        private Dictionary<int, DialogueLine> dialogueLines = new Dictionary<int, DialogueLine>();
        private int currentIndex = 0;

        /// <summary>
        /// The choice class contains all the data of a node in the dialogue
        /// </summary>
        public class Choice
        {
            /// <summary>
            /// Used internally. The identifier of a choice
            /// </summary>
            public int id;
            /// <summary>
            /// The dialogue line
            /// </summary>
            public string dialogue;
            /// <summary>
            /// Which speaker is saying this line
            /// </summary>
            public string speaker;
            /// <summary>
            /// UserData defined in the editor
            /// </summary>
            public string userData;
        }

        /// <summary>
        /// Used internally. Adds a line to the dialogue.
        /// </summary>
        /// <param name="line">The line to be added</param>
        public void AddLine(DialogueLine line)
        {
            dialogueLines.Add(line.id, line);
        }

        /// <summary>
        /// Resets the dialogue to the start
        /// </summary>
        public void Start()
        {
            // set up the start of the dialogue
            currentIndex = 0;
        }

        /// <summary>
        /// Gets all the possible choices for the current dialogue node
        /// </summary>
        /// <returns>An array of type Choice containing all the possible choices</returns>
        public Choice[] GetChoices()
        {
            List<Choice> choices = new List<Choice>();

            foreach (int id in dialogueLines[currentIndex].output)
            {
                Choice c = new Choice();
                c.id = id;
                c.dialogue = dialogueLines[id].dialogue;
                c.speaker = dialogueLines[id].speaker;
                c.userData = dialogueLines[id].userData;
                choices.Add(c);
            }

            return choices.ToArray();
        }
        /// <summary>
        /// Get the line with the same id as the parameter
        /// </summary>
        /// <param name="id">id of the line to get</param>
        /// <returns>Return the line if exists else return null</returns>
        public DialogueLine GetLine(int id)
        {
            if (id < 0 || id >= dialogueLines.Count)
                return null;
            return dialogueLines[id];
        }
        /// <summary>
        /// Advanced the dialogue with the given choice. Could also be used to jump to a different position in the dialogue, but this is not recommended.
        /// </summary>
        /// <param name="c"></param>
        public void PickChoice(Choice c)
        {
            currentIndex = c.id;
        }
        public DialogueLine GetCurrentLine()
        {
            return dialogueLines[currentIndex];
        }

        public bool GoNextLine()
        {
            DialogueLine currentLine = dialogueLines[currentIndex];
            if (currentLine.output.Count != 1)
                return false;
            currentIndex = currentLine.output[0];
            return true;
        }
        public bool GoToLine(int id)
        {
            if (!dialogueLines.ContainsKey(id))
                return false;
            currentIndex = id;
            return true;
        }
    }

    /// <summary>
    /// The DialogueManager is the main class to interface with the stored dialogues.
    /// <example>
    /// How to get a dialogue from script
    /// <code>
    /// DialogueManager manager = DialogueManager.LoadDialogueFile(dialogueFile);
    /// Dialogue dialogue = manager.GetDialogue("SampleDialogue");
    /// </code>
    /// </example>
    /// </summary>
    public class EasyDialogueManager
    {
        private static Dictionary<EasyDialogueFile, EasyDialogueManager> managers = new Dictionary<EasyDialogueFile, EasyDialogueManager>();
        static Dialogue currentDialogue;
        Dialogue.Choice currentChoice;
        float speedsmooth = 0.05f; // your speed smooth, but not time
        private float myAlpha = 0f;
        bool pauseFlag = false;
        bool isStarted = false;
        bool speechUpdated = true;
        public bool isFinished = false;

        static EasyFile easyFile = null;

        private EasyDM easyBExecution = null;
        public EasyDM EasyBExecution
        {
            get { return easyBExecution; }
            set { easyBExecution = value; }
        }


        /// <summary>
        /// Load a dialogue file from the resources folder.
        /// </summary>
        /// <param name="resourcePath">The path in the Resource folder</param>
        /// <returns>A DialogueManager holding the file reference</returns>
        /// 
        public EasyDialogueManager()
        {
        }
        public EasyDialogueManager(string fileName)
        {
            //LoadDialogueFile (fileName);
        }

        /*  public static EasyDialogueManager LoadDialogueFile(string resourcePath)
          {
                  LoadDialogueFileFromPath1 (resourcePath);
              DialogueFile importedFile =  deserializeFromXML(resourcePath);

                  EasyDialogueFile f = null;
                  try{
                       f = LoadDialogueFileFromPath1 (resourcePath);
                  }catch(Exception e)
                  {

                  }
                  DialogueFile importedFile1 = null;
                  return LoadDialogueFile(importedFile1);

          }
          */
        public static DialogueFile LoadDialogueFileFromPath(string resourcePath)
        {
            DialogueFile importedFile = deserializeFromXML(resourcePath);
            return importedFile;

        }
        public static EasyDialogueFile LoadDialogueFileFromPath1(string resourcePath)
        {
            EasyFile importedFile = LoadEasyFileFromPath(resourcePath);


            EasyDialogueFile dialFile = new EasyDialogueFile(importedFile.name);
            dialFile.entries = importedFile.entries;
            dialFile.lines = importedFile.lines;
            return dialFile;

        }
        /// <summary>
        /// Load a dialogue file by the given DialogueFile reference
        /// </summary>
        /// <param name="file">A reference to a DialogueFile</param>
        /// <returns>A DialogueManager holding the file reference</returns>
        public static EasyDialogueManager LoadDialogueFile(DialogueFile file)
        {

            /*  if (managers.ContainsKey(file))
                  return managers[file];
      */
            // load file, optimize for searching dialogues
            EasyDialogueManager manager = new EasyDialogueManager();
            /*        managers.Add(file, manager);

                    manager.file = file;
            */
            return manager;
        }
        public static EasyDialogueManager LoadDialogueFile(EasyDialogueFile file)
        {

            if (managers.ContainsKey(file))
                return managers[file];

            // load file, optimize for searching dialogues
            EasyDialogueManager manager = new EasyDialogueManager();
            managers.Add(file, manager);

            manager.easyDialogueFile = file;

            return manager;
        }
        private DialogueFile file;
        private EasyDialogueFile easyDialogueFile;

        /// <summary>
        /// Retreives a specific dialogue from the manager
        /// </summary>
        /// <param name="dialogueName">The name of the dialogue</param>
        /// <returns>A Dialogue containing all the lines</returns>
        public Dialogue GetDialogue(string dialogueName)
        {
            // create the dialogue and return it
            Dialogue result = new Dialogue();

            // get the lines
            foreach (DialogueLine line in easyDialogueFile.lines)
            {
                if (line.dialogueEntry == dialogueName)
                    result.AddLine(line);
            }

            return result;
        }

        static NLP.DialogueFile deserializeFromXML(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(NLP.DialogueFile));


            TextReader textReader = new StreamReader(filename);
            NLP.DialogueFile result = serializer.Deserialize(textReader) as NLP.DialogueFile;
            textReader.Close();
            return result;
        }
        static EasyFile deserializeFromXML1(string filename)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(EasyFile));


            TextReader textReader = new StreamReader(filename);
            EasyFile result = serializer.Deserialize(textReader) as EasyFile;
            textReader.Close();
            return result;
        }
        public virtual void speek(Dialogue currentDialogue)
        {


        }
        public virtual void receive(Dialogue.Choice choice)
        {
            currentDialogue.PickChoice(choice);
            currentChoice = choice;
        }
        public virtual void receive(string utternace)
        {
        }
        public virtual void init(string dialogueFile)
        {
            currentDialogue = GetDialogue("TutorialStart");
            currentChoice = currentDialogue.GetChoices()[0];

            currentDialogue.PickChoice(currentChoice);
            Console.WriteLine("dialogue initialized");
        }

        public virtual Dialogue process()
        {

            if (!isStarted)
            {
                init("");
                if (currentChoice.dialogue != null)
                    Console.WriteLine(currentChoice.dialogue);
                isStarted = true;
                speechUpdated = true;
            }
            else
            {  // pauseFlag = false;
                //if (currentChoice != null)
                {

                    if (currentDialogue.GetChoices().Length > 1)
                    {
                        pauseFlag = true;
                        speechUpdated = true;

                    }
                    else
                    {
                        if (!pauseFlag && myAlpha > 0)
                        {
                            myAlpha = myAlpha - speedsmooth ;  /* Time.deltaTime;*/
                        }
                        else if (myAlpha <= 0)
                        {
                            if (currentDialogue.GetChoices().Length == 1)
                            {
                                //	if (GUILayout.Button("Next"))
                                currentChoice = null;
                                currentChoice = currentDialogue.GetChoices()[0];
                                if (currentChoice != null)
                                {
                                    currentDialogue.PickChoice(currentChoice);
                                    if (currentChoice.dialogue != null)
                                    {
                                        Console.WriteLine(currentChoice.dialogue);
                                        speechUpdated = true;
                                    }

                                }
                            }
                        }
                    }
                }
            }
            if (speechUpdated)
            {
                myAlpha = 5f;
                speechUpdated = false;
            }


            return currentDialogue;
        }

        public static Dialogue getCurrentDialogue()
        {
            return currentDialogue;
        }







        public static EasyFile LoadEasyFileFromPath(string path)
        {

            XDocument doc = new XDocument();

            try
            {
                doc = XDocument.Load(path);
            }
            catch (Exception e)
            {

                Console.Write("exception found");
            }
            XElement _root = doc.Root;
            Console.WriteLine("processing loadDialogueMatcher  : " + _root.Name);



            //file.


            foreach (XElement currentNode in _root.Elements())
            {
                if (currentNode.Name.ToString() == "name")
                {
                    easyFile = new EasyFile(currentNode.Value.ToString());

                }
                else if (currentNode.Name.ToString() == "entries")
                {
                    processDialogueEntries(ref easyFile, currentNode);
                }
                else if (currentNode.Name.ToString() == "lines")
                {
                    processDialogueLines(ref easyFile, currentNode);
                }

            }

            return easyFile;

        }
        static void processDialogueEntries(ref EasyFile file, XElement entriesNode)
        {
            foreach (XElement dialogueEntryNode in entriesNode.Elements())
            {
                DialogueEntry entry = new DialogueEntry();
                foreach (XElement node in dialogueEntryNode.Elements())
                {

                    if (node.Name.ToString() == "id")
                    {
                        entry.id = node.Value.ToString();
                    }
                    if (node.Name.ToString() == "maxLineId")
                    {
                        string noOfLines = node.Value.ToString();
                        int m = -1;
                        try
                        {
                            m = Int32.Parse(noOfLines);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        entry.maxLineId = m;

                    }
                    if (node.Name.ToString() == "speakers")
                    {
                        foreach (XElement speakerNode in node.Elements())
                        {
                            if (speakerNode.Name.ToString() == "string")
                            {
                                entry.speakers.Add(speakerNode.Value.ToString());
                            }

                        }
                    }


                }
                file.entries.Add(entry);

            }






        }
        static void processDialogueLines(ref EasyFile file, XElement lines)
        {
            foreach (XElement line in lines.Elements())
            {
                DialogueLine dLine = new DialogueLine();
                foreach (XElement node in line.Elements())
                {
                    if (node.Name.ToString() == "id")
                    {
                        string id = node.Value.ToString();
                        try
                        {
                            dLine.id = Int32.Parse(id);
                        }
                        catch (FormatException e)
                        {
                            Console.WriteLine(e.Message);
                            dLine.id = -1;
                        }
                    }

                    if (node.Name.ToString() == "dialogueEntry")
                    {
                        dLine.dialogueEntry = node.Value.ToString();
                    }
                    if (node.Name.ToString() == "dialogue")
                    {
                        dLine.dialogue = node.Value.ToString();
                    }
                    if (node.Name.ToString() == "output")
                    {
                        foreach (XElement outputLine in node.Elements())
                        {
                            if (outputLine.Name.ToString() == "int")
                            {
                                string id = outputLine.Value.ToString();
                                try
                                {
                                    dLine.output.Add(Int32.Parse(id));
                                }
                                catch (FormatException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                            }

                        }
                    }
                    if (node.Name.ToString() == "userData")
                    {
                        dLine.userData = node.Value.ToString();
                    }
                    if (node.Name.ToString() == "speaker")
                    {
                        dLine.speaker = node.Value.ToString();
                    }

                }
                file.lines.Add(dLine);
            }
        }




    }





















}
