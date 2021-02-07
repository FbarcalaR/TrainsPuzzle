public class TurnRail : Rail {
    public override bool CanMoveDown() {
        return transform.eulerAngles.z == 0 || transform.eulerAngles.z == 90;
    }

    public override bool CanMoveLeft() {
        return transform.eulerAngles.z == 0 || transform.eulerAngles.z == 270;
    }

    public override bool CanMoveRight() {
        return transform.eulerAngles.z == 90 || transform.eulerAngles.z == 180;
    }

    public override bool CanMoveUp() {
        return transform.eulerAngles.z == 180 || transform.eulerAngles.z == 270;
    }
}
