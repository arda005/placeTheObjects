using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CaseProject.UI
{
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance { get; private set; }

        [SerializeField] private RectTransform content;
        [SerializeField] private RectTransform popupContent;

        private List<MenuController> menus;

        private void Awake()
        {
            Instance = this;
            menus = new List<MenuController>(Resources.LoadAll<MenuController>(string.Empty));
        }

        private void Start()
        {
            OpenMenu<MainMenuController>();
        }

        public void DestroyMenus()
        {

        }

        public T OpenMenu<T>() where T : MenuController
        {
            return OpenMenu<T>(false);
        }

        public T OpenMenu<T>(bool closeOthers) where T : MenuController
        {
            if (typeof(T).IsSubclassOf(typeof(FullScreenController)))
            {
                return OpenFullScreenMenu<T>(closeOthers);
            }
            if (typeof(T).IsSubclassOf(typeof(PopupController)))
            {
                return OpenPopupMenu<T>(closeOthers);
            }
            else
            {
                Debug.LogError("Unexpected class type");
                return null;
            }
        }

        private T OpenFullScreenMenu<T>(bool closeOthers) where T : MenuController
        {
            return (T)CreateMenu<T>(content, closeOthers);
        }

        private T OpenPopupMenu<T>(bool closeOthers) where T : MenuController
        {
            popupContent.gameObject.SetActive(true);
            return (T)CreateMenu<T>(popupContent, closeOthers);
        }

        private MenuController CreateMenu<T>(RectTransform parent, bool closeOthers) where T : MenuController
        {
            var isAlreadyOpened = parent.GetComponentInChildren<T>() != null;
            if (isAlreadyOpened) return null;

            var menu = FindMenu<T>();

            if (menu == null) return null;

            //*******************  WARNING  *******************
            //After this line this fuction must be able to create menus. So,
            //all null returnings should be above this line!!!
            if (closeOthers)
                DestroyAllMenus(parent);

            var createdObject = Instantiate(menu, parent);
            return createdObject;
        }

        public void CreateMenuInstant(MenuController menuContent)
        {
            DestroyAllFullScreenMenus();
            var createdObject = Instantiate(menuContent, content);
        }

        public void DestroyAllFullScreenMenus()
        {
            DestroyAllMenus(content);
        }

        public void DestroyAllMenus(RectTransform parent)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }

        public T FindMenu<T>() where T : MenuController
        {
            foreach (var item in menus)
            {
                var component = item.GetComponent<T>();
                if (component != null)
                    return component;
            }

            return null;
        }

        public void CloseMenu(FullScreenController fullScreenContent)
        {
            Destroy(fullScreenContent);
            //UpdateContent(content);
        }

        public int GetActivePopupCount()
        {
            return popupContent.childCount;
        }

        public void CheckPopupContentActivity()
        {
            popupContent.gameObject.SetActive(GetActivePopupCount() > 1);
        }
    }
}