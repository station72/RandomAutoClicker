using System.Threading.Tasks;

namespace RandomAutoClicker.Model.Clicker.ClickBehaviour
{
    public class LeftButtonClickBehaviour : ClickBehaviourBase
    {
        public override async Task ClickAsync(int xPos, int yPos)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, xPos, yPos, 0, 0);
            await Task.Delay(50);
            mouse_event(MOUSEEVENTF_LEFTUP, xPos, yPos, 0, 0);
        }
    }
}
