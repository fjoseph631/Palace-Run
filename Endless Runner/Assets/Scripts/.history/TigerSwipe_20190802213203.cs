public class TigerSwipe : MonoBehaviour {
 
 if(GameManager.getManager().getDirection().Count>0)
        {
            if(GameManager.getManager().getDirection().Peek()!=GameManager.turnDirection.Straight)
            {
                if(swipe.tag =="GameController")
                {
                    GameManager.getManager().setSwipe(true);
                    Debug.Log("Tiger Can Turn ");
                }
            }
        }   
}