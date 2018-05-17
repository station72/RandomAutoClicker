using System.Threading.Tasks;

namespace RandomAutoClicker.Model.Clicker.ClickBehaviour
{
    public class DoubleClickBehaviour : ClickBehaviourBase
    {
        private readonly ClickBehaviourBase _click;

        public DoubleClickBehaviour(ClickBehaviourBase clickBehaviuor)
        {
            _click = clickBehaviuor;
        }

        public override async Task ClickAsync(int xPos, int yPos)
        {
            await _click.ClickAsync(xPos, yPos);
            await Task.Delay(50);
            await _click.ClickAsync(xPos, yPos);
        }
    }
}
