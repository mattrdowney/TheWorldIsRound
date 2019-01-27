using UnityEngine;
using Planetaria;

namespace TheWorldIsRound
{
    public class Bird : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            main_character = GameObject.FindObjectOfType<Bird>().gameObject.transform;
            main_controller = GameObject.FindObjectOfType<PlanetariaActuator>().gameObject.internal_game_object.transform;
            planetaria_rigidbody = GameObject.FindObjectOfType<PlanetariaRigidbody>();

#if UNITY_EDITOR
            GameObject.FindObjectOfType<PlanetariaActuator>().input_device_type = PlanetariaActuator.InputDevice.Mouse;
#else
            GameObject.FindObjectOfType<PlanetariaActuator>().input_device_type = PlanetariaActuator.InputDevice.Gyroscope;
            GameObject.FindObjectOfType<PlanetariaActuator>().virtual_reality_tracker_type = UnityEngine.XR.XRNode.Head;
#endif
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // The direction from the bird to the controller (which represents either the head's view direction or the mouse position).
            Vector3 direction = Bearing.attractor(main_character.forward, main_controller.forward);
            float target_angle = Vector3.SignedAngle(main_character.up, direction, main_character.forward) * Mathf.Deg2Rad;
            // Distance from satellite to controller (which represents either the head's view direction or the mouse position).
            float target_distance = Vector3.Angle(main_character.forward, main_controller.forward) * Mathf.Deg2Rad;
            target_distance = Mathf.Clamp01(target_distance / Mathf.PI); /*satellite radius, not divided by 2 because Actuator goes twice as far as head movement*/
            // Return the composite input direction.
            Vector2 input_direction = new Vector2(-Mathf.Sin(target_angle), Mathf.Cos(target_angle)) * target_distance;

            // add velocity based on input
            planetaria_rigidbody.relative_velocity += input_direction * Time.deltaTime;
            Vector2 velocity = planetaria_rigidbody.relative_velocity;

            if (planetaria_rigidbody.relative_velocity.sqrMagnitude > 1)
            {
                planetaria_rigidbody.relative_velocity = planetaria_rigidbody.relative_velocity.normalized;
            }
        }

        [SerializeField] [HideInInspector] private Transform main_character;
        [SerializeField] [HideInInspector] private Transform main_controller;
        [SerializeField] [HideInInspector] private PlanetariaRigidbody planetaria_rigidbody;
    }
}

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.