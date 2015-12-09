using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Tank_Game
{
    class CoinPile : GridEntity
    {
        private int value = -1;
        private int lifeTime = -1;
        private int appearTime = -1;
        private int disappearTime = -1;

        public CoinPile(Point p, int lifeTime, int appearTime, int value) : base(p)
        {
            this.setName("C  ");
            this.setValue(value);
            this.setLifeTime(lifeTime);
            this.setAppearTime(appearTime);
            this.setDisappearTime(appearTime, lifeTime);
        }

        public int getValue()
        {
            return value;
        }

        public void setValue(int value)
        {
            this.value = value;
        }

        public void setLifeTime(int value)
        {
            this.lifeTime = value;
        }

        public int getLifeTime()
        {
            return lifeTime;
        }

        public int getAppearTime()
        {
            return appearTime;
        }

        public void setAppearTime(int value)
        {
            this.appearTime = value;
        }

        public int getDisappearTime()
        {
            return disappearTime;
        }

        public void setDisappearTime(int appearTime, int lifeTime)
        {
            this.disappearTime = appearTime + lifeTime;
        }
    }
}
