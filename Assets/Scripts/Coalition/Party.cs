using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Party")] 
public class Party : ScriptableObject
{
    public enum Tags
    {
        Expensive,
        Cultural,
        WorkersRights,
        Military,
        Diplomatic,
        IndividualFreedom,
        BigBusiness,
        Industrial,
        Environmental,
        Friendly,
        LowCosts,

    }

   

    public string partyName;
    public string partyDescription;

    public Sprite partyLogo;

    public string leaderName;
    public string leaderDescription;

    public Tags[] partyLikes;
    public Tags[] partyDislikes;

    public int totalSeats;

    public Color partyColor;

    

    
 
}
