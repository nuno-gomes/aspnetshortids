using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;

namespace NunoGomes.Web.Configuration
{
    public class NamingContainerControlCollection : ControlCollection
    {
        #region Private Fields
        /// <summary>
        /// Provides the link between the Id and Name
        /// </summary>
        private Dictionary<string, string> m_linkDictionary = null;
        /// <summary>
        /// Provides a collection of controls by name.
        /// </summary>
        private Dictionary<string, System.Web.UI.Control> m_nameDictionary = null;
        private int _controlCounter = 0;
        #endregion Private Fields

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="NamingContainerControlCollection"/> class.
        /// </summary>
        /// <param name="owner">The ASP.NET server control that the control collection is created for.</param>
        /// <exception cref="T:System.ArgumentException">Occurs if the owner parameter is null. </exception>
        public NamingContainerControlCollection(Control owner)
            : base(owner)
        {
            m_linkDictionary = new Dictionary<string, string>();
            m_nameDictionary = new Dictionary<string, System.Web.UI.Control>();
            _controlCounter = 0;
        }
        #endregion Constructors

        #region Overriden Methods
        /// <summary>
        /// Removes all controls from the current server control's <see cref="T:System.Web.UI.ControlCollection"></see> object.
        /// </summary>
        public override void Clear()
        {
            m_nameDictionary.Clear();
            m_linkDictionary.Clear();
            _controlCounter = 0;
            base.Clear();
        }

        /// <summary>
        /// Removes a child control, at the specified index location, from the <see cref="T:System.Web.UI.ControlCollection"></see> object.
        /// </summary>
        /// <param name="index">The ordinal index of the server control to be removed from the collection.</param>
        /// <exception cref="T:System.Web.HttpException">Thrown if the <see cref="T:System.Web.UI.ControlCollection"></see> is read-only. </exception>
        public override void RemoveAt(int index)
        {
            m_nameDictionary.Remove(m_linkDictionary[this[index].ID]);
            m_linkDictionary.Remove(this[index].ID);

            base.RemoveAt(index);
        }
        #endregion Overriden Methods

        #region Public Methods

        /// <summary>
        /// Finds a control.
        /// </summary>
        /// <param name="text">Either a control id or name.</param>
        /// <param name="pathOffset">The path offset.</param>
        /// <returns></returns>
        public System.Web.UI.Control FindControl(string text, int pathOffset)
        {
            char[] chArray1 = new char[] { '$', ':' };
            int num1 = text.IndexOfAny(chArray1, pathOffset);
            string text1 = null;
            System.Web.UI.Control ctrl = null;
            if (num1 == -1)
            {
                text1 = text.Substring(pathOffset);
            }
            else
            {
                text1 = text.Substring(pathOffset, num1 - pathOffset);
            }

            if (ContainsName(text1))
            {
                ctrl = m_nameDictionary[text1];
            }
            if (ContainsID(text1))
            {
                ctrl = m_nameDictionary[m_linkDictionary[text1]]; ;
            }
            if (ctrl != null && num1 != -1)
            {
                ctrl = ctrl.FindControl(text.Substring(num1 + 1));
            }
            return ctrl;
        }

        /// <summary>
        /// Gets the unique control sufix.
        /// </summary>
        /// <returns></returns>
        public int GetUniqueControlSufix()
        {
            return _controlCounter++;
        }

        /// <summary>
        /// Determines whether a control is in the <see cref="NamingContainerControlCollection"></see>.
        /// </summary>
        /// <param name="name">The controls name.</param>
        /// <returns>
        /// 	<c>true</c> if a control is found; otherwise, <c>false</c>.
        /// </returns>
        public bool ContainsName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            return m_nameDictionary.ContainsKey(name);
        }

        /// <summary>
        /// Gets the control name given the control id.
        /// </summary>
        /// <param name="shortid">The control's name.</param>
        /// <returns></returns>
        public string GetName(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return id;
            }
            if (ContainsID(id))
            {
                string name = m_linkDictionary[id];
                //if (baseid == shortid)
                //{
                //    return null;
                //}
                return name;
            }
            return id;
        }

        /// <summary>
        /// Registers the pair [name, control] and link id to name.
        /// </summary>
        /// <param name="id">The id value.</param>
        /// <param name="name">The name value.</param>
        /// <param name="control">The control.</param>
        public void RegisterControl(string id, string name, Control control)
        {
            m_nameDictionary.Add(name, control);
            m_linkDictionary.Add(id, name);
        }

        #endregion Public Methods

        #region Private Methods
        /// <summary>
        /// Determines whether a control is in the <see cref="NamingContainerControlCollection"></see>.
        /// </summary>
        /// <param name="id">The controls id.</param>
        /// <returns>
        /// 	<c>true</c> if a control is found; otherwise, <c>false</c>.
        /// </returns>
        private bool ContainsID(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return false;
            }
            return m_linkDictionary.ContainsKey(id);
        }
        #endregion Private Methods
    }
}
