using UnityEngine;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;

/**
 * GestureLibrary class works in simple steps:
 * - If it is not a web player, then copy the gesture XML file to persistent data path if it is not already there.
 * - Load the XML file from persistent data path (or resources folders if it is a web player) and create a list of gestures.
 * - If the user wants to save a new gesture, add the gesture to the list of gestures and to the XML file, then save it.
 * 
 * AN IMPORTANT NOTE: GestureLibrary's add gesture feature does not save to the XML file.
 * For this, you need to write a server sided script and call it inside AddGesture method.
 */
public class GestureLibrary {

    private string libraryName;
    private string libraryFilename;
    private string persistentLibraryPath;
    private string xmlContents;
    private XmlDocument gestureLibrary = new XmlDocument();
    private List<Gesture> library = new List<Gesture>();

    public List<Gesture> Library { get { return library; } }


    public GestureLibrary(string libraryName) {
        this.libraryName = libraryName;
        this.libraryFilename = libraryName + ".xml";
        this.persistentLibraryPath = System.IO.Path.Combine(Application.persistentDataPath, libraryFilename);
        
        if (!Application.isWebPlayer) {
            CopyToPersistentPath();
        }
        
        LoadLibrary();
    }


    /**
     * Loads the library from xml to a list of gestures
     */
    public void LoadLibrary() {

        /**
         * Load XML
         */
        string xmlContents = "";

        #if !UNITY_WEBPLAYER
            xmlContents = FileTools.Read(persistentLibraryPath);
        #else
            xmlContents = Resources.Load<TextAsset>(libraryName).text;
        #endif

        gestureLibrary.LoadXml(xmlContents);


        /**
         * Get "gesture" elements
         */
        XmlNodeList xmlGestureList = gestureLibrary.GetElementsByTagName("gesture");

        /**
         * Parse "gesture" elements and add them to library
         */
        foreach (XmlNode xmlGestureNode in xmlGestureList) {

            string gestureName = xmlGestureNode.Attributes.GetNamedItem("name").Value;
            XmlNodeList xmlPoints = xmlGestureNode.ChildNodes;
            List<Vector2> gesturePoints = new List<Vector2>();

            foreach (XmlNode point in xmlPoints) {

                Vector2 gesturePoint = new Vector2();
                gesturePoint.x = (float)System.Convert.ToDouble(point.Attributes.GetNamedItem("x").Value);
                gesturePoint.y = (float)System.Convert.ToDouble(point.Attributes.GetNamedItem("y").Value);
                gesturePoints.Add(gesturePoint);

            }

            Gesture gesture = new Gesture(gesturePoints, gestureName);
            library.Add(gesture);
        }
    }


    /**
     * Adds a new gesture to library and then saves it to the xml.
     * The trick here is that we don't reload the newly saved xml.
     * It would have been a waste of resources. Instead, we just add
     * the new gesture to the list of gestures (the library).
     */
    public bool AddGesture(Gesture gesture) {

        /**
         * Create the xml node to add to the xml file
         */
        XmlElement rootElement = gestureLibrary.DocumentElement;
        XmlElement gestureNode = gestureLibrary.CreateElement("gesture");
        gestureNode.SetAttribute("name", gesture.Name);

        foreach (Vector2 v in gesture.Points) {
            XmlElement gesturePoint = gestureLibrary.CreateElement("point");
            gesturePoint.SetAttribute("x", v.x.ToString());
            gesturePoint.SetAttribute("y", v.y.ToString());

            gestureNode.AppendChild(gesturePoint);
        }

        /**
         * Append the node to xml file contents
         */
        rootElement.AppendChild(gestureNode);

        try {

            /**
             * Add the new gesture to the list of gestures
             */
            this.Library.Add(gesture);

            /**
             * Save the file if it is not the web player, because
             * web player cannot have write permissions.
             */
            #if !UNITY_WEBPLAYER
                FileTools.Write(persistentLibraryPath, gestureLibrary.OuterXml);
            #endif

            return true;
        } catch (Exception e) {
            Debug.Log(e.Message);
            return false;
        }

    }


    /**
     * Copy to persistent data path so that we can save a new gesture
     * on all platforms (except web player)
     */
    private void CopyToPersistentPath() {

        #if !UNITY_WEBPLAYER
            if (!FileTools.Exists(persistentLibraryPath)) {
                string fileContents = Resources.Load<TextAsset>(libraryName).text;
                FileTools.Write(persistentLibraryPath, fileContents);
            }
        #endif
        
    }


} // end of GestureLibrary