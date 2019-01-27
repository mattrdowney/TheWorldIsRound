using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Planetaria;

namespace TheWorldIsRound
{
    /// <summary>
    /// 
    /// </summary>
    /// <see cref="https://www.nature.com/articles/srep26756/figures/4"/> // binning light function for light from sun (and light polution) based on angle of incidence from sun.
    /// <seealso cref=""/> // Trying to find the research article that goes over 1) direct lighting, 2) indirect lighting (due to atmospheric refraction), and 3) ? reflective lighting due to surface properties
    public class SunlightPlanetarium : WorldPlanetarium
    {
        public SunlightPlanetarium(Vector3 point)
        {
            this.point = point;

            pixel_centroids = new NormalizedCartesianCoordinates[0];
        }
        
        public override void set_pixels(Color32[] positions) { }

        public override Color32[] get_pixels(NormalizedCartesianCoordinates[] positions)
        {
            Color32[] colors = new Color32[positions.Length];
            for (int index = 0; index < positions.Length; ++index)
            {
                Vector3 position = positions[index].data;
                float angle_in_degrees = Vector3.Angle(position, point);
                int color_index = Mathf.Clamp((sunlight_lookup_table.Length - 1) - Mathf.FloorToInt(angle_in_degrees/2), 0, (sunlight_lookup_table.Length - 1)); // avoid negatives
                colors[index] = sunlight_lookup_table[color_index];
            }
            return colors;
        }

#if UNITY_EDITOR
        public override void save(string file_name) { }
#endif

        private Vector3 point;

        // cache
        private float dot_product_threshold;
        // hardcode array of sunlight intensities from https://www.nature.com/articles/srep26756/figures/4 Figure #4 (texture read would work, but there are disadvantages)
        private readonly Color32[] sunlight_lookup_table = new Color32[90]
        {
            new Color32(129, 99, 99, 255), // -90 degrees below horizon
            new Color32(130, 100, 100, 255),
            new Color32(131, 101, 101, 255),
            new Color32(132, 102, 102, 255),
            new Color32(133, 103, 103, 255),
            new Color32(134, 104, 104, 255), // -80 degrees ...
            new Color32(135, 105, 105, 255),
            new Color32(136, 106, 106, 255),
            new Color32(137, 107, 107, 255),
            new Color32(138, 108, 108, 255),
            new Color32(139, 109, 109, 255), // -70 degrees ...
            new Color32(140, 110, 110, 255),
            new Color32(141, 111, 111, 255),
            new Color32(142, 112, 112, 255),
            new Color32(143, 113, 113, 255),
            new Color32(144, 114, 114, 255), // -60 degrees ...
            new Color32(145, 115, 115, 255),
            new Color32(146, 116, 116, 255),
            new Color32(147, 117, 117, 255),
            new Color32(148, 118, 118, 255),
            new Color32(149, 119, 117, 255), // -50 degrees ...
            new Color32(150, 120, 116, 255),
            new Color32(151, 121, 115, 255),
            new Color32(152, 122, 114, 255),
            new Color32(153, 123, 113, 255),
            new Color32(154, 124, 112, 255), // -40 degrees ...
            new Color32(155, 125, 111, 255),
            new Color32(156, 126, 110, 255),
            new Color32(157, 127, 109, 255),
            new Color32(158, 128, 108, 255),
            new Color32(159, 129, 107, 255), // -30 degrees ...
            // PREVIOUS VALUES EXTRAPOLATED, LATER VALUES EMPIRICALLY DETERMINED
            new Color32(160, 130, 106, 255), //-28 degrees below the horizon
            new Color32(161, 135, 102, 255),
            new Color32(162, 138, 100, 255),
            new Color32(159, 137, 98, 255),
            new Color32(154, 131, 113, 255),
            new Color32(146, 130, 137, 255), // -18 degrees ...
            new Color32(111, 128, 174, 255),
            new Color32(86, 130, 193, 255),
            new Color32(92, 142, 203, 255), // -12 degrees ...
            new Color32(110, 155, 210, 255),
            new Color32(133, 170, 219, 255),
            new Color32(161, 189, 228, 255), // -6 degrees ...
            new Color32(179, 202, 233, 255),
            new Color32(192, 212, 236, 255),
            new Color32(201, 218, 238, 255), // 0 degrees - at horizon
            new Color32(206, 223, 241, 255),
            new Color32(215, 227, 241, 255),
            new Color32(222, 227, 237, 255),
            new Color32(234, 230, 227, 255),
            new Color32(238, 231, 225, 255),
            new Color32(239, 235, 226, 255),
            new Color32(249, 230, 224, 255),
            new Color32(251, 235, 221, 255),
            new Color32(246, 238, 225, 255),
            new Color32(243, 237, 232, 255),
            new Color32(244, 237, 227, 255),
            new Color32(247, 241, 229, 255),
            new Color32(247, 242, 222, 255), // +26 degrees above the horizon
            // PREVIOUS VALUES EMPIRICALLY DETERMINED, LATER VALUES EXTRAPOLATED
            new Color32(248, 244, 223, 255), // +28 degrees ...
            new Color32(249, 245, 224, 255),
            new Color32(250, 247, 225, 255),
            new Color32(251, 248, 226, 255),
            new Color32(252, 250, 227, 255),
            new Color32(253, 251, 228, 255),
            new Color32(254, 253, 229, 255),
            new Color32(255, 254, 230, 255), // +42 degrees above horizon
            new Color32(255, 255, 231, 255), // +44 degrees above horizon
            new Color32(255, 255, 232, 255),
            new Color32(255, 255, 233, 255),
            new Color32(255, 255, 235, 255),
            new Color32(255, 255, 236, 255),
            new Color32(255, 255, 237, 255),
            new Color32(255, 255, 238, 255),
            new Color32(255, 255, 239, 255),
            new Color32(255, 255, 240, 255), // +60 degrees ...
            new Color32(255, 255, 241, 255),
            new Color32(255, 255, 242, 255),
            new Color32(255, 255, 243, 255),
            new Color32(255, 255, 244, 255),
            new Color32(255, 255, 245, 255),
            new Color32(255, 255, 247, 255),
            new Color32(255, 255, 248, 255),
            new Color32(255, 255, 249, 255),
            new Color32(255, 255, 250, 255),
            new Color32(255, 255, 251, 255), // +80 degrees above horizon
            new Color32(255, 255, 252, 255),
            new Color32(255, 255, 253, 255),
            new Color32(255, 255, 254, 255),
            new Color32(255, 255, 255, 255), // +88 (< +90) degrees above horizon
        };
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