﻿using UnityEngine;
            int powerMove = MyGameManager.instance.ActivePlayer.power;
            MyGameManager.instance.ActivePlayer.power = 0;
            MyGameManager.instance.ActivePlayer.MyScore.text = (0).ToString();
            MyGameManager.instance.SwitchActivePlayer();
            MyGameManager.instance.ActivePlayer.Move(powerMove);
        }