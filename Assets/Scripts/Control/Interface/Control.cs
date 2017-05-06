using System;
using System.Collections.Generic;

public interface IControl
{
    float GetDelayOfMoveSteps();
    void AttackMove(string AttackType, Character gameObject);
    void DefenderMove(Character gameObject);
    bool OtherMove();
}



