using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
//using Assets.Tests;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Tests
{
    [TestClass()]
    public class DragTests: MonoBehaviour
    {
        private Vector2 pointerOffset;
        private RectTransform canvasRectTransform;
        private RectTransform panelRectTransform;

        [TestMethod()]
        public void OnPointerDownTest()
        {
            GivenCanvas();
            GivenCanvasRectTransform(new RectTransform()
            {
                anchoredPosition = new Vector2(3.3f, 3.3f),
                localPosition = new Vector2(23f, 23f),
                anchorMax = new Vector2(2.5f, 2.5f),
                anchorMin = new Vector2(2.1f, 2.1f),
                offsetMax = new Vector2(5.5f, 5.5f),
                offsetMin = new Vector2(2.2f, 2.2f),
                pivot = new Vector2(7.5f, 7.5f),
                sizeDelta = new Vector2(2f, 2f)
            });
            GivenPanelRectTransform(new RectTransform()
            {
                anchoredPosition = new Vector2(3.3f, 3.3f),
                localPosition = new Vector2(23f, 23f),
                anchorMax = new Vector2(2.5f, 2.5f),
                anchorMin = new Vector2(2.1f, 2.1f),
                offsetMax = new Vector2(5.5f, 5.5f),
                offsetMin = new Vector2(2.2f, 2.2f),
                pivot = new Vector2(7.5f, 7.5f),
                sizeDelta = new Vector2(2f, 2f)
            });
            GivenPointerEventData();
        }
        #region GIVEN
        public void GivenCanvas()
        {
            Canvas canvas = GetComponentInParent<Canvas>();
        }
        public void GivenCanvasRectTransform(RectTransform canvas)
        {
            canvasRectTransform = canvas as RectTransform;
        }
        public void GivenPanelRectTransform(RectTransform panel)
        {
            panelRectTransform = panel.parent as RectTransform;
        }
        public void GivenPointerEventData(PointerEventData data)
        {

        }
        #endregion
        #region WHEN
        #endregion
        #region THEN
        #endregion
    }
}