using System.Threading.Tasks;

namespace RandomAutoClicker.Model.Clicker.ClickBehaviour
{
    public class EmptyClickBehaviour : ClickBehaviourBase
    {
        public override Task ClickAsync(int xPos, int yPos)
        {
            return Task.CompletedTask;
        }
    }
}
