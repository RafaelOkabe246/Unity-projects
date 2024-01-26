public enum BehaviourState 
{
    Engage,      //Try to atttack player
    EngageRange, //Range attack
    Follow,     //Follow the player by a distance
    Retreat,    //Go to the nearest grid corner
    Idle        //Stay in same place
}
