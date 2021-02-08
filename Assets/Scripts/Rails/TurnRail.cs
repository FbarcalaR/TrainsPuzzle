public class TurnRail : Rail {
    public override bool CanMoveDown() {
        return (transform.eulerAngles.z > -10 && transform.eulerAngles.z < 10)
            || (transform.eulerAngles.z > 80 && transform.eulerAngles.z < 100);
    }

    public override bool CanMoveLeft() {
        return (transform.eulerAngles.z > -10 && transform.eulerAngles.z < 10)
            || (transform.eulerAngles.z > 260 && transform.eulerAngles.z < 280);
    }

    public override bool CanMoveRight() {
        return (transform.eulerAngles.z > 80 && transform.eulerAngles.z < 100)
         || (transform.eulerAngles.z > 170 && transform.eulerAngles.z < 190);
    }

    public override bool CanMoveUp() {
        return (transform.eulerAngles.z > 170 && transform.eulerAngles.z < 190)
         || (transform.eulerAngles.z > 260 && transform.eulerAngles.z < 280);
    }
}
