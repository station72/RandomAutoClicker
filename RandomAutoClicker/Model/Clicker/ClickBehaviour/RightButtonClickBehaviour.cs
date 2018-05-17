using System.Threading.Tasks;

namespace RandomAutoClicker.Model.Clicker.ClickBehaviour
{
    public class RightButtonClickBehaviour : ClickBehaviourBase
    {
        public override async Task ClickAsync(int xPos, int yPos)
        {
            mouse_event(MOUSEEVENTF_RIGHTDOWN, xPos, yPos, 0, 0);
            await Task.Delay(50);
            mouse_event(MOUSEEVENTF_RIGHTTUP, xPos, yPos, 0, 0);
        }
    }
}
