using UnityEngine;

[DisallowMultipleComponent] //so that an object can have only one of this script.
public class SineWaveMovement : MonoBehaviour
{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 1f, 5f);
    [SerializeField] float period = 2f; //2 seconds.

    #region Explanation
    //0 for not moved, 1 for fully moved the amount specified in the movementvector.
    #endregion 
    [Range(0f, 1f)] [SerializeField] float movementFactor; //using this just for a visual representation of the cycle.
    Vector3 startingPos;

    private void Start() => startingPos = transform.position;

    private void Update()
    {
        #region Explenation
        //so that the stuff below doesn't happen if the value of period is very close to or less than zero. you shouldn't
        //compare floats. two floats will most likely vary by a very tiny amount. therefore, it's unpredictable to use ==
        //with floats, so always specify the acceptable difference. since we want to protect against period being 0, we can
        //say <= mathf.epsilon.
        #endregion
        if (period <= Mathf.Epsilon) return;

        #region Explanation
        //number of full cycles elapsed. the value of this grows continually from 0 a period is the time it takes to
        //complete one full cycle. if the period is 2 seconds and time.time is 1 second, then we've gone through 0.5
        //cycles. because we're using time.time directly, this is automatically framerate independant. a period's value
        //shouldn't be 0 or else you'll get a nan exception.
        #endregion
        float cyclesAmount = Time.time / period;
        #region Explanation
        //a unit circle's radian amount is 2pi radians aka tau radians because tau = 2pi. so this will take you all the
        //way round the circle. therefore, we're using it because we want full cycles.
        #endregion
        const float tau = Mathf.PI * 2;
        #region Explanation
        //because we want a full cycle aka sin going from -1 to 1, we're putting in tau aka 2pi radians and specifying
        //the speed(the time it take for an object to complete a full cycle) of one full cycle aka a period. it's called
        //rawsinewave because it goes from -1 to 1. the reason we're multiplying tau by cyclesamount and not just period
        //is because, depending on the number of the cycles that have passed, the value of tau should be different. for
        //example, if we want half a cycle, we're putting in pi, if we want a full cycle, we're putting in 2pi and so on.
        //that's the reason the object can have multiple cycles and still move on an appropriate path/distance. if we put
        //in just period and not cyclesamount, the object wouldn't move.

        //depending on the number of the angle(the stuff in ()) we put in in the sin function, the value is gonna go from
        //-1 to 1, which is what makes the sine wave. so as the value of the angle grows, we get a sine wave.
        #endregion
        float rawSinWave = Mathf.Sin(cyclesAmount * tau); //goes from -1 to 1.
        #region Explanation
        //we want it o go from 0 to 1, so we're dividing -1 and 1 by 2, which gives us -0.5 and 0.5, so we're adding 0.5
        //on top to make it go from 0 to 1. we want it to start at 0 so that the object goes from it's current position to
        //another specified place.
        #endregion
        movementFactor = rawSinWave / 2f + 0.5f; 

        Vector3 offset = movementFactor * movementVector;
        #region Explanation
        //we're using = and storing startingpos because it's better to set the position every frame than to increment it 
        //by using just += offset.
        #endregion
        transform.position = startingPos + offset; 
    }
}