using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CaseProject.UI
{
    /// <summary>
    /// Opens full screen or pop up menus and control them.
    /// </summary>
    public class MenuManager : MonoBehaviour
    {
        public static MenuManager Instance { get; private set; }

        /// <summary>
        /// Parent for general menus. For now just full screen menus.
        /// </summary>
        [SerializeField] private RectTransform content;

        /// <summary>
        /// Parent for pop up menus. 
        /// </summary>
        [SerializeField] private RectTransform popupContent;

        /// <summary>
        /// All menus contained in Resources folder.
        /// </summary>
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

        /// <summary>
        /// Opens menu with specified type.
        /// </summary>
        /// <typeparam name="T">Menu type</typeparam>
        /// <param name="closeOthers">Should close other menus with same type.</param>
        /// <returns>Created menu</returns>
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

        public T OpenMenu<T>() where T : MenuController
        {
            return OpenMenu<T>(false);
        }

        /// <summary>
        /// Opens full screen menu.
        /// </summary>
        private T OpenFullScreenMenu<T>(bool closeOthers) where T : MenuController
        {
            return (T)CreateMenu<T>(content, closeOthers);
        }

        /// <summary>
        /// Opens Pop up menu.
        /// </summary>
        private T OpenPopupMenu<T>(bool closeOthers) where T : MenuController
        {
            popupContent.gameObject.SetActive(true);
            return (T)CreateMenu<T>(popupContent, closeOthers);
        }

        /// <summary>
        /// Creates menu in the specified parent.
        /// </summary>
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

        /// <summary>
        /// Creates passed menu. It is useful for calling in unity inspector.
        /// </summary>
        /// <param name="menuContent">Menu that going to be created</param>
        public void CreateMenuInstant(MenuController menuContent)
        {
            DestroyAllFullScreenMenus();
            var createdObject = Instantiate(menuContent, content);
        }

        /// <summary>
        /// Destroys all full screen menus.
        /// </summary>
        public void DestroyAllFullScreenMenus()
        {
            DestroyAllMenus(content);
        }

        /// <summary>
        /// Destroys all menus in a parent.
        /// </summary>
        public void DestroyAllMenus(RectTransform parent)
        {
            for (int i = 0; i < parent.childCount; i++)
            {
                Destroy(parent.GetChild(i).gameObject);
            }
        }

        /// <summary>
        /// Finds a menu with specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Closes the menu. Useful for calling in unity inspector.
        /// </summary>
        /// <param name="fullScreenContent"></param>
        public void CloseMenu(FullScreenController fullScreenContent)
        {
            Destroy(fullScreenContent);
        }

        /// <summary>
        /// Returns how many pop up open right now.
        /// </summary>
        /// <returns>Current pop up count.</returns>
        public int GetActivePopupCount()
        {
            return popupContent.childCount;
        }

        /// <summary>
        /// Updates popup content if needed.
        /// </summary>
        public void UpdatePopupContentActivity()
        {
            popupContent.gameObject.SetActive(GetActivePopupCount() > 1);
        }
    }
}