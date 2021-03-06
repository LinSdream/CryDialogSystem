﻿/**********************************************************
*Author: wangjiaying
*Date: 2016.6.23
*Func:
**********************************************************/

using UnityEngine;
using Event = UnityEngine.Event;

namespace CryDialog.Editor
{
    public class Tools
    {

        /// <summary>
        /// 检查鼠标是否位于指定Rect之内
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool IsValidMouseAABB(Rect area)
        {
            return Event.current.mousePosition.x > area.x && Event.current.mousePosition.y > area.y && Event.current.mousePosition.x < area.xMax && Event.current.mousePosition.y < area.yMax;
        }

        public static bool MouseIsInContent()
        {
            return Event.current.mousePosition.x > DialogEditorWindow.DialogWindow._leftWidth && Event.current.mousePosition.y > DialogEditorWindow.DialogWindow._titleHeight;
        }

        public static void DrawBazier(Vector2 startPos, Vector2 endPos, Color color)
        {
            DrawBazier(startPos, endPos, color, new Color(1, 1, 1, 0.3f));
        }

        public static void DrawBazier(Vector2 startPos, Vector2 endPos)
        {
            DrawBazier(startPos, endPos, new Color(1, 1, 1, 0.4f), new Color(1, 1, 1, 0.3f));
        }

        public static void DrawBazier(Vector2 startPos, Vector2 endPos, Color color, Color color2, float inSize = 2f, float outSize = 5f)
        {
            float tgl = Vector2.Distance(startPos, endPos) * 0.4f;
            Vector3 startTangent = startPos + Vector2.right * tgl;
            Vector3 endTangent = endPos - Vector2.right * tgl;
            UnityEditor.Handles.DrawBezier(startPos, endPos, startTangent, endTangent, color, null, inSize);
            UnityEditor.Handles.DrawBezier(startPos, endPos, startTangent, endTangent, color2, null, outSize);

            //Draw arrow
            UnityEditor.Handles.DrawLine(endPos + new Vector2(-10, -6), endPos);
            UnityEditor.Handles.DrawLine(endPos + new Vector2(-10, 6), endPos);
        }


        public static bool MouseDown { get { if (Event.current != null) return Event.current.type == EventType.MouseDown; return false; } }
        public static bool MouseUp { get { if (Event.current != null) return Event.current.type == EventType.MouseUp; return false; } }
        public static bool MouseDrag { get { if (Event.current != null) return Event.current.type == EventType.MouseDrag; return false; } }
        public static bool MouseDoubleClick { get { if (MouseDown) { return Event.current.clickCount > 1; } return false; } }


        public static Rect CalcLeftLinkRect(Rect missionRect)
        {
            float size = 25 * Zoom;
            return new Rect(new Vector2(missionRect.min.x, missionRect.min.y + 12 * Zoom), new Vector2(size, size));
        }

        public static Rect CalcRightLinkRect(Rect missionRect)
        {
            float size = 25 * Zoom;
            return new Rect(new Vector2(missionRect.max.x - size, missionRect.min.y + 12 * Zoom), new Vector2(size, size));
        }


        public const float _nodeWidth = 200f;
        public const float _nodeHeight = 50f;
        public const float _nodeHalfHeight = 25f;
        public static float NodeHalfHeightZoomed { get { return _nodeHeight * 0.5f * DialogEditorWindow.DialogWindow.Zoom; } }
        public static float Zoom { get { return DialogEditorWindow.DialogWindow.Zoom; } }
        public static Rect GetNodeRect(Vector2 realPos)
        {
            float zoom = DialogEditorWindow.DialogWindow.Zoom;
            return new Rect(realPos.x * zoom, realPos.y * zoom, _nodeWidth * zoom, _nodeHeight * zoom);
        }

        public static Rect GetNodeRect(Vector2 realPos, string extraText)
        {
            float zoom = DialogEditorWindow.DialogWindow.Zoom;
            Rect rect = new Rect(realPos.x * zoom, realPos.y * zoom, _nodeWidth * zoom, _nodeHeight * zoom);

            GUIContent des = new GUIContent(extraText);

            //计算额外描述高度
            GUIStyle desStyle = ResourcesManager.GetInstance.GetOverflowFontStyle(12);
            float height = desStyle.CalcHeight(des, rect.width);

            rect.height = rect.height + height + 10;

            return rect;
        }

    }
}