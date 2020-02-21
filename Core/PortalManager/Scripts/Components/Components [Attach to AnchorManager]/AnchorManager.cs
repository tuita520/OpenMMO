﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using OpenMMO;
using OpenMMO.Network;
using OpenMMO.Database;
using OpenMMO.Portals;
using OpenMMO.Debugging;

namespace OpenMMO.Portals
{

	// ===================================================================================
	// AnchorManager
	// ===================================================================================
	[DisallowMultipleComponent]
	public partial class AnchorManager : MonoBehaviour
	{
	
		public static List<PortalAnchorEntry> portalAnchors = new List<PortalAnchorEntry>();
		public static List<GameObject> startAnchors = new List<GameObject>();
		
		// -------------------------------------------------------------------------------
        // OnDestroy
        // -------------------------------------------------------------------------------
		void OnDestroy()
		{
			portalAnchors.Clear();
			startAnchors.Clear();
		}
		
        // ============================ START ANCHORS ===================================
        
        // -------------------------------------------------------------------------------
        // GetArchetypeStartPosition
        // -------------------------------------------------------------------------------
        public static Transform GetArchetypeStartPosition(GameObject player)
        {

			PlayerComponent pc = player.GetComponent<PlayerComponent>();

			startAnchors.Shuffle();
			
            foreach (GameObject anchor in startAnchors)
            {

                StartAnchor sc = anchor.GetComponent<StartAnchor>();

                foreach (ArchetypeTemplate template in sc.archeTypes)
                {
                    if (template == pc.archeType)
                    {
						DebugManager.LogFormat(nameof(AnchorManager), nameof(GetArchetypeStartPosition), anchor.name); //DEBUG
                        return anchor.transform;
					}
				}
            }
			
			DebugManager.LogFormat(nameof(AnchorManager), nameof(GetArchetypeStartPosition), "NOT FOUND"); //DEBUG
            return player.transform;

        }
        
        // -------------------------------------------------------------------------------
    	// RegisterStartAnchor
    	// @Client / @Server
    	// -------------------------------------------------------------------------------
        public static void RegisterStartAnchor(GameObject anchor)
        {
            startAnchors.Add(anchor);
            DebugManager.LogFormat(nameof(AnchorManager), nameof(RegisterStartAnchor), anchor.name); //DEBUG
        }

        // -------------------------------------------------------------------------------
    	// UnRegisterStartAnchor
    	// @Client / @Server
    	// -------------------------------------------------------------------------------
        public static void UnRegisterStartAnchor(GameObject anchor)
        {
           for (int i = 0; i < startAnchors.Count; i++)
           {
           		if (startAnchors[i].name == anchor.name)
           		{
           			startAnchors.RemoveAt(i);
           			DebugManager.LogFormat(nameof(AnchorManager), nameof(UnRegisterStartAnchor), anchor.name); //DEBUG
           		}
           	}
        }
		
        // ============================ PORTAL ANCHORS ===================================
        
        // -------------------------------------------------------------------------------
    	// CheckPortalAnchor
    	// @Client / @Server
    	// -------------------------------------------------------------------------------
        public static bool CheckPortalAnchor(string _name)
        {
        
        	if (String.IsNullOrWhiteSpace(_name))
        		return false;
        	
        	foreach (PortalAnchorEntry anchor in portalAnchors)
        	{
        		if (anchor.name == _name)
        		{
        			DebugManager.LogFormat(nameof(AnchorManager), nameof(CheckPortalAnchor), anchor.name); //DEBUG
        			return true;
				}
			}
			
			DebugManager.LogFormat(nameof(AnchorManager), nameof(CheckPortalAnchor), _name, "NOT FOUND"); //DEBUG
			return false;
        }
        
        // -------------------------------------------------------------------------------
    	// GetPortalAnchorPosition
    	// @Client / @Server
    	// -------------------------------------------------------------------------------
        public static Vector3 GetPortalAnchorPosition(string _name)
        {
        
        	foreach (PortalAnchorEntry anchor in portalAnchors)
        	{
        		if (anchor.name == _name)
				{
					DebugManager.LogFormat(nameof(AnchorManager), nameof(GetPortalAnchorPosition), anchor.name); //DEBUG
					return anchor.position;
				}
			}
			
			DebugManager.LogFormat(nameof(AnchorManager), nameof(GetPortalAnchorPosition), _name, "NOT FOUND"); //DEBUG
			return Vector3.zero;
        }
        
        // -------------------------------------------------------------------------------
    	// RegisterPortalAnchor
    	// @Client / @Server
    	// -------------------------------------------------------------------------------
        public static void RegisterPortalAnchor(string _name, Vector3 _position)
        {
            portalAnchors.Add(
            				new PortalAnchorEntry
            				{
            					name = _name,
            					position = _position
            				}
            );
            
            DebugManager.LogFormat(nameof(AnchorManager), nameof(RegisterPortalAnchor), _name); //DEBUG
        }

        // -------------------------------------------------------------------------------
    	// UnRegisterPortalAnchor
    	// @Client / @Server
    	// -------------------------------------------------------------------------------
        public static void UnRegisterPortalAnchor(string _name)
        {
           
           for (int i = 0; i < portalAnchors.Count; i++)
           {
           		if (portalAnchors[i].name == _name)
           		{
           			portalAnchors.RemoveAt(i);
            		DebugManager.LogFormat(nameof(AnchorManager), nameof(UnRegisterPortalAnchor), _name); //DEBUG
            	}
            }
            
        }

    	// -------------------------------------------------------------------------------
    	
	}

}

// =======================================================================================