using Assets.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.UI;

public class CustomUIManager : MonoBehaviour
{

    public enum FrameKey
    {
        FRAME_GREEN = 2,
        FRAME_YELLOW = 3,
        FRAME_BLUE = 1
    }

    public enum AvatarKey
    {
        LIGHT_BLUE_FEMALE_HERO = 1,
        YELLOW_MALE_HERO = 2,
        GRAY_MALE_HERO = 3,
        LIGHT_BLUE_MALE_HERO= 4,
        VIOLET_MALE_HERO = 5,
        VIOLET_FEMALE_HERO = 6,
        PURPLE_FEMALE_HERO = 7,
        RED_FEMALE_HERO = 8,
        YELLOW_FEMALE_HERO = 9
    }

    public enum UserSelection
    {
        AVATAR = 1,
        FRAME = 2,
    }

    
    [SerializeField]
    [Tooltip("All hero sprite dictionary key value")]
    private KeySpriteDictionary spriteDictionary;

    [SerializeField]
    [Tooltip("All frame sprite dictionary key value")]
    private KeySpriteDictionary frameDictionary;

    [SerializeField]
    private GameObject avatarSprite;

    [SerializeField]
    private GameObject frameSprite;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PressFrameButton(GameObject objectClick) {

        Debug.Log("Entering in method PressFrameButton");
        Debug.Log("Key " + objectClick.gameObject.name);

        FrameKey frame = objectClick.GetComponent<FrameItem>().ItemKey;
        Sprite sprite;
        switch (frame)
        {
            case FrameKey.FRAME_BLUE:

                sprite = frameDictionary["Frame_Blue"];

                ModifyUserSelectionAvatar(sprite, UserSelection.FRAME);

                break;
            case FrameKey.FRAME_GREEN:
                sprite = frameDictionary["Frame_Green"];

                ModifyUserSelectionAvatar(sprite, UserSelection.FRAME);

                break;
            case FrameKey.FRAME_YELLOW:

                sprite = frameDictionary["Frame_Yellow"];

                ModifyUserSelectionAvatar(sprite, UserSelection.FRAME);

                break;
        }

    }

    public void PressHeroButton(GameObject objectClick)
    {
        Debug.Log("Entering in method PressHeroButton");
        Debug.Log("Key "+ objectClick.gameObject.name);

        AvatarKey avatar = objectClick.GetComponent<HeroItem>().ItemKey;
        Sprite sprite;
        switch (avatar)
        {
            case AvatarKey.GRAY_MALE_HERO:

                sprite = spriteDictionary["Gray_Male_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;
            case AvatarKey.LIGHT_BLUE_FEMALE_HERO:

                sprite = spriteDictionary["Blue_Female_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;
            case AvatarKey.LIGHT_BLUE_MALE_HERO:

                sprite = spriteDictionary["Light_Blue_Male_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

           case AvatarKey.PURPLE_FEMALE_HERO:

                sprite = spriteDictionary["Purple_Female_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

            case AvatarKey.RED_FEMALE_HERO:

                sprite = spriteDictionary["Red_Female_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

            case AvatarKey.VIOLET_FEMALE_HERO:

                sprite = spriteDictionary["Violet_Female_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

            case AvatarKey.VIOLET_MALE_HERO:

                sprite = spriteDictionary["Violet_Male_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

            case AvatarKey.YELLOW_FEMALE_HERO:

                sprite = spriteDictionary["Yellow_Female_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

            case AvatarKey.YELLOW_MALE_HERO:

                sprite = spriteDictionary["Yellow_Male_Hero"];

                ModifyUserSelectionAvatar(sprite, UserSelection.AVATAR);

                break;

        }

    }


    private void ModifyUserSelectionAvatar(Sprite sprite, UserSelection selection ) {

        if (selection.Equals(UserSelection.FRAME)) {

            Image image = frameSprite.GetComponent<Image>();

            image.sprite = sprite;
        }

        if (selection.Equals(UserSelection.AVATAR))
        {
            Image image = avatarSprite.GetComponent<Image>();

            image.sprite = sprite;
        }

    }


}
