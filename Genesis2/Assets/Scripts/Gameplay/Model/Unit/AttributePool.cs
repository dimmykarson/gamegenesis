using UnityEngine;
using System.Collections.Generic;

namespace Gameplay.Attribute
{
    public class AttributePool : MonoBehaviour
    {
        private Dictionary<AttributeType, Attribute> attributes = new Dictionary<AttributeType, Attribute>();

        private void Awake()
        {
            Attribute[] childAttributes = transform.GetComponentsInChildren<Attribute>();
            attributes = new Dictionary<AttributeType, Attribute>(childAttributes.Length);
            for (int i = 0; i < childAttributes.Length; i++)
            {
                Attribute childAttribute = childAttributes[i];
                if (attributes.ContainsKey(childAttribute.AttributeType))
                {
                    Debug.Log("Duplicated attribute " + childAttribute.AttributeType, this);
                    continue;
                }
                attributes.Add(childAttribute.AttributeType, childAttribute);
            }
        }

        public Attribute GetAttribute(AttributeType targetAttributeType)
        {
            if (!attributes.ContainsKey(targetAttributeType))
            {
                Debug.Log("Can't found any attribute of type: " + targetAttributeType, this);
                return null;
            }
            return attributes[targetAttributeType];
        }
    }
}
