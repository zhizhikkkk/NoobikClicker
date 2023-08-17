using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine.Events;
//using Spine.Unity;
//using UnityEngine.UI.Extensions;

namespace NubikClicker
{
[ExecuteAlways]
public class NubikAnimPro : MonoBehaviour
{
    [OnValueChanged("ValueChanged")]
    public List<someAnimation> someAnimation;
    bool checkFrameSecond = false;
    bool check = true;
    bool once = true;
    int x = 0;
    int index = 0;
    float frameTime;
    float eachTime;
    int mainIndex;
    GameObject coll;
    EventSystem Event;
    Sequence seq1;

    [HideInInspector]
    public bool playing = false;

    [FoldoutGroup("Other")]
    [Sirenix.OdinInspector.ReadOnly]
    public string whatType = "CanvasRenderer";

    [FoldoutGroup("Other")]
    [Sirenix.OdinInspector.ReadOnly]
    public RectTransform rect;

    //[FoldoutGroup("Other")]
    //[Sirenix.OdinInspector.ReadOnly]
    //public SkeletonAnimation mainSkeleton;
    [FoldoutGroup("Other")]
    [Sirenix.OdinInspector.ReadOnly]
    public Renderer mainRenderer;
    [FoldoutGroup("Other")]
    [Sirenix.OdinInspector.ReadOnly]
    public MeshRenderer mainMeshRenderer;
    [FoldoutGroup("Other")]
    [Sirenix.OdinInspector.ReadOnly]
    public SpriteRenderer mainSpriteRenderer;
    [FoldoutGroup("Other")]
    [Sirenix.OdinInspector.ReadOnly]
    public CanvasRenderer mainCanvasRenderer;
    [FoldoutGroup("Other")]
    [Sirenix.OdinInspector.ReadOnly]
    public Text mainText;
    [FoldoutGroup("Other")]
    [Sirenix.OdinInspector.ReadOnly]
    public Image mainImage;
    [FoldoutGroup("Other")]
    [Sirenix.OdinInspector.ReadOnly]
    public CanvasGroup mainCanvasGroup;
    [FoldoutGroup("Other")]
    [Sirenix.OdinInspector.ReadOnly]
    public Outline mainOutline;
    //[FoldoutGroup("Other")]
    //[Sirenix.OdinInspector.ReadOnly]
    //public NicerOutline mainNicerOutline;
    //[FoldoutGroup("Other")]
    //[Sirenix.OdinInspector.ReadOnly]
    //public BestFitOutline mainBestFitOutline;
    [FoldoutGroup("Other")]
    [Sirenix.OdinInspector.ReadOnly]
    public EventTrigger mainEventTrigger;
    [FoldoutGroup("Other")]
    [Sirenix.OdinInspector.ReadOnly]
    public BoxCollider2D mainBoxCollider;
    //[FoldoutGroup("Other")]
    //[Sirenix.OdinInspector.ReadOnly]
    //public SkeletonGraphic mainSkeletonGraphic;

    [Sirenix.OdinInspector.ReadOnly]
    public bool componentGetted = false;

    Color startRendererClr;
    float startRendererAlpha;

    private void Awake()
    {
        if (Application.isPlaying)
        {
            if (!componentGetted)
            {
                GetType();
                Debug.Log("Component not getted", gameObject);
            }
        }
    }

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            if(someAnimation != null)
            {
                for (int w = 0; w < someAnimation.Count; w++)
                {
                    if (someAnimation[w].playOnStart)
                    {
                        mainIndex = w;
                        someAnimation[mainIndex].Play = true;
                        break;
                    }
                }
            }
        }
    }

    private void Start()
    {
        if (Application.isPlaying)
        {
            if(mainRenderer != null)
            {
                startRendererClr = mainRenderer.material.color;
                startRendererAlpha = mainRenderer.material.color.a;
            }
        }
    }

    private void Update()
    {
        if (!Application.isPlaying)
        {
            if (!componentGetted)
            {
                GetType();
            }
        }
        if (Application.isPlaying)
        {
            if (someAnimation.Count > 0)
            {
                if (checkFrameSecond)
                {
                    if (once)
                    {
                        once = false;
                        frameTime = someAnimation[mainIndex].al[x].eachFrameDuration;
                        eachTime = someAnimation[mainIndex].al[x].eachFrameDuration;
                        someAnimation[mainIndex].al[x].frames[index].SetActive(true);
                    }
                    if (index < someAnimation[mainIndex].al[x].frames.Length)
                    {
                        eachTime -= Time.deltaTime;
                        if (eachTime <= 0)
                        {
                            someAnimation[mainIndex].al[x].eachFrameDuration = frameTime;
                            eachTime = frameTime;
                            index++;
                            if (index < someAnimation[mainIndex].al[x].frames.Length)
                            {
                                someAnimation[mainIndex].al[x].frames[index].SetActive(true);
                            }
                            if (someAnimation[mainIndex].al[x].offPreviousFrame)
                            {
                                someAnimation[mainIndex].al[x].frames[index - 1].SetActive(false);
                            }
                        }
                    }
                    else
                    {
                        checkFrameSecond = false;
                        if (someAnimation[mainIndex].al[x].offAllFrame)
                        {
                            if (someAnimation[mainIndex].al[x].offPreviousFrame == false)
                            {
                                for (int r = 0; r < someAnimation[mainIndex].al[x].frames.Length; r++)
                                {
                                    someAnimation[mainIndex].al[x].frames[r].SetActive(false);
                                }
                            }
                        }
                        Complete();
                    }
                }
                if (someAnimation[mainIndex].Play)
                {
                    if (check)
                    {
                        check = false;
                        playing = true;
                        if (someAnimation[mainIndex].al[x].checkDot)
                        {
                            if (someAnimation[mainIndex].al[x].durationDot == 0)
                            {
                                someAnimation[mainIndex].al[x].durationDot = 0.01f;
                            }
                            if (mainCanvasRenderer == null && mainSpriteRenderer == null && mainMeshRenderer == null && mainRenderer == null)
                            {
                                Invoke("Complete", someAnimation[mainIndex].al[x].intervalTime + someAnimation[mainIndex].al[x].durationDot);
                                whatType = "SpriteRenderer";
                            }
                            else
                            {
                                if (whatType == "CanvasRenderer")
                                {
                                    seq1 = DOTween.Sequence().SetUpdate(true);
                                    seq1.AppendInterval(someAnimation[mainIndex].al[x].intervalTime);
                                    seq1.AppendInterval(0);
                                    //seq1.Append(rect.DOScale(rect.localScale, 0.01f));
                                    if (someAnimation[mainIndex].al[x].enableJumpAnchorPos)
                                    {
                                        seq1.Append(rect.DOJumpAnchorPos(someAnimation[mainIndex].al[x].jumpPosition, someAnimation[mainIndex].al[x].jumpPower, 1, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                    }
                                    if (someAnimation[mainIndex].al[x].offPosition == false)
                                    {
                                        if (someAnimation[mainIndex].al[x].addPosition)
                                        {
                                            Vector3 anotherPos = new Vector3(rect.anchoredPosition.x, rect.anchoredPosition.y, 0) + someAnimation[mainIndex].al[x].position;
                                            if (someAnimation[mainIndex].al[x].linear)
                                            {
                                                seq1.Append(rect.DOAnchorPos(anotherPos, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                            }
                                            else
                                            {
                                                seq1.Append(rect.DOAnchorPos(anotherPos, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                            }
                                        }
                                        else
                                        {
                                            if (someAnimation[mainIndex].al[x].enableJumpAnchorPos == false)
                                            {
                                                if (someAnimation[mainIndex].al[x].linear)
                                                {
                                                    seq1.Append(rect.DOAnchorPos(someAnimation[mainIndex].al[x].position, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                }
                                                else
                                                {
                                                    seq1.Append(rect.DOAnchorPos(someAnimation[mainIndex].al[x].position, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                }
                                            }
                                        }
                                    }
                                    if (someAnimation[mainIndex].al[x].offRotation == false)
                                    {
                                        if (someAnimation[mainIndex].al[x].Simple)
                                        {
                                            if (someAnimation[mainIndex].al[x].linear)
                                            {
                                                seq1.Join(rect.DORotate(someAnimation[mainIndex].al[x].rotation, someAnimation[mainIndex].al[x].durationDot, RotateMode.Fast).SetEase(Ease.Linear));
                                            }
                                            else
                                            {
                                                seq1.Join(rect.DORotate(someAnimation[mainIndex].al[x].rotation, someAnimation[mainIndex].al[x].durationDot, RotateMode.Fast).SetEase(someAnimation[mainIndex].al[x].ease));
                                            }
                                        }
                                        if (someAnimation[mainIndex].al[x].Beyond360)
                                        {
                                            if (someAnimation[mainIndex].al[x].linear)
                                            {
                                                seq1.Join(rect.DORotate(someAnimation[mainIndex].al[x].rotation, someAnimation[mainIndex].al[x].durationDot, RotateMode.FastBeyond360).SetEase(Ease.Linear));
                                            }
                                            else
                                            {
                                                seq1.Join(rect.DORotate(someAnimation[mainIndex].al[x].rotation, someAnimation[mainIndex].al[x].durationDot, RotateMode.FastBeyond360).SetEase(someAnimation[mainIndex].al[x].ease));
                                            }
                                        }
                                    }
                                    if (!someAnimation[mainIndex].al[x].offFade)
                                    {
                                        if (mainText != null)
                                        {
                                            if (someAnimation[mainIndex].al[x].color)
                                            {
                                                if (someAnimation[mainIndex].al[x].linear)
                                                {
                                                    seq1.Join(mainText.DOColor(someAnimation[mainIndex].al[x].clr, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                }
                                                else
                                                {
                                                    seq1.Join(mainText.DOColor(someAnimation[mainIndex].al[x].clr, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                }
                                            }
                                            if (mainCanvasGroup != null)
                                            {
                                                if (someAnimation[mainIndex].al[x].linear)
                                                {
                                                    seq1.Join(mainCanvasGroup.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                }
                                                else
                                                {
                                                    seq1.Join(mainCanvasGroup.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                }
                                            }
                                            else
                                            {
                                                if (someAnimation[mainIndex].al[x].linear)
                                                {
                                                    seq1.Join(mainText.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                }
                                                else
                                                {
                                                    seq1.Join(mainText.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                }
                                            }
                                            if (mainOutline != null)
                                            {
                                                if (someAnimation[mainIndex].al[x].Fade == 0)
                                                {
                                                    if (someAnimation[mainIndex].al[x].linear)
                                                    {
                                                        seq1.Join(mainOutline.DOFade(someAnimation[mainIndex].al[x].Fade / 3, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                    }
                                                    else
                                                    {
                                                        seq1.Join(mainOutline.DOFade(someAnimation[mainIndex].al[x].Fade / 3, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                    }
                                                }
                                                else
                                                {
                                                    if (someAnimation[mainIndex].al[x].linear)
                                                    {
                                                        seq1.Join(mainOutline.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                    }
                                                    else
                                                    {
                                                        seq1.Join(mainOutline.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                    }
                                                }
                                            }
                                            //if (mainNicerOutline != null)
                                            //{
                                            //    if (mainCanvasGroup != null)
                                            //    {
                                            //        mainNicerOutline.effectColor = new Color(mainNicerOutline.effectColor.r, mainNicerOutline.effectColor.g, mainNicerOutline.effectColor.b, mainCanvasGroup.alpha);
                                            //    }
                                            //    else
                                            //    {
                                            //        mainNicerOutline.effectColor = new Color(mainNicerOutline.effectColor.r, mainNicerOutline.effectColor.g, mainNicerOutline.effectColor.b, mainText.color.a);
                                            //    }

                                            //    if (someAnimation[mainIndex].al[x].linear)
                                            //    {
                                            //        if (someAnimation[mainIndex].al[x].Fade == 0)
                                            //        {
                                            //            Color clr = mainNicerOutline.effectColor;
                                            //            clr = new Color(0.4f, 0.4f, 0.4f, 0f);
                                            //            clr.a = 0;
                                            //            seq1.Join(DOTween.To(() => mainNicerOutline.effectColor, x => mainNicerOutline.effectColor = x, clr, someAnimation[mainIndex].al[x].durationDot / 1.5f).OnUpdate(Update).SetEase(Ease.Linear));
                                            //        }
                                            //        else
                                            //        {
                                            //            Color clr = Color.black;
                                            //            clr.a = someAnimation[mainIndex].al[x].Fade;
                                            //            seq1.Join(DOTween.To(() => mainNicerOutline.effectColor, x => mainNicerOutline.effectColor = x, clr, someAnimation[mainIndex].al[x].durationDot / 1.5f).OnUpdate(Update).SetEase(Ease.Linear));
                                            //        }
                                            //    }
                                            //    else
                                            //    {
                                            //        if (someAnimation[mainIndex].al[x].Fade == 0)
                                            //        {
                                            //            Color clr = mainNicerOutline.effectColor;
                                            //            clr = new Color(0.4f, 0.4f, 0.4f, 0f);
                                            //            clr.a = 0;
                                            //            seq1.Join(DOTween.To(() => mainNicerOutline.effectColor, x => mainNicerOutline.effectColor = x, clr, someAnimation[mainIndex].al[x].durationDot / 1.5f).OnUpdate(Update).SetEase(someAnimation[mainIndex].al[x].ease));
                                            //        }
                                            //        else
                                            //        {
                                            //            Color clr = Color.black;
                                            //            clr.a = someAnimation[mainIndex].al[x].Fade;
                                            //            seq1.Join(DOTween.To(() => mainNicerOutline.effectColor, x => mainNicerOutline.effectColor = x, clr, someAnimation[mainIndex].al[x].durationDot / 1.5f).OnUpdate(Update).SetEase(someAnimation[mainIndex].al[x].ease));
                                            //        }
                                            //    }
                                            //}
                                            //if (mainBestFitOutline != null)
                                            //{
                                            //    if (mainCanvasGroup != null)
                                            //    {
                                            //        //mainBestFitOutline.effectColor = new Color(mainBestFitOutline.effectColor.r, mainBestFitOutline.effectColor.g, mainBestFitOutline.effectColor.b, mainCanvasGroup.alpha);
                                            //    }
                                            //    else
                                            //    {
                                            //        mainBestFitOutline.effectColor = new Color(mainBestFitOutline.effectColor.r, mainBestFitOutline.effectColor.g, mainBestFitOutline.effectColor.b, mainText.color.a);

                                            //        if (someAnimation[mainIndex].al[x].linear)
                                            //        {
                                            //            if (someAnimation[mainIndex].al[x].Fade == 0)
                                            //            {
                                            //                Color clr = mainBestFitOutline.effectColor;
                                            //                clr = new Color(0.4f, 0.4f, 0.4f, 0f);
                                            //                clr.a = 0;
                                            //                seq1.Join(DOTween.To(() => mainBestFitOutline.effectColor, x => mainBestFitOutline.effectColor = x, clr, someAnimation[mainIndex].al[x].durationDot / 1.5f).OnUpdate(Update).SetEase(Ease.Linear));
                                            //            }
                                            //            else
                                            //            {
                                            //                Color clr = Color.black;
                                            //                clr.a = someAnimation[mainIndex].al[x].Fade;
                                            //                seq1.Join(DOTween.To(() => mainBestFitOutline.effectColor, x => mainBestFitOutline.effectColor = x, clr, someAnimation[mainIndex].al[x].durationDot / 1.5f).OnUpdate(Update).SetEase(Ease.Linear));
                                            //            }
                                            //        }
                                            //        else
                                            //        {
                                            //            if (someAnimation[mainIndex].al[x].Fade == 0)
                                            //            {
                                            //                Color clr = mainBestFitOutline.effectColor;
                                            //                clr = new Color(0.4f, 0.4f, 0.4f, 0f);
                                            //                clr.a = 0;
                                            //                seq1.Join(DOTween.To(() => mainBestFitOutline.effectColor, x => mainBestFitOutline.effectColor = x, clr, someAnimation[mainIndex].al[x].durationDot / 1.5f).OnUpdate(Update).SetEase(someAnimation[mainIndex].al[x].ease));
                                            //            }
                                            //            else
                                            //            {
                                            //                Color clr = Color.black;
                                            //                clr.a = someAnimation[mainIndex].al[x].Fade;
                                            //                seq1.Join(DOTween.To(() => mainBestFitOutline.effectColor, x => mainBestFitOutline.effectColor = x, clr, someAnimation[mainIndex].al[x].durationDot / 1.5f).OnUpdate(Update).SetEase(someAnimation[mainIndex].al[x].ease));
                                            //            }
                                            //        }
                                            //    }
                                            //}
                                        }
                                        else
                                        {
                                            if (mainImage != null)
                                            {
                                                //if (mainNicerOutline != null)
                                                //{
                                                //    if (mainCanvasGroup != null)
                                                //    {
                                                //        mainNicerOutline.effectColor = new Color(mainNicerOutline.effectColor.r, mainNicerOutline.effectColor.g, mainNicerOutline.effectColor.b, mainCanvasGroup.alpha);
                                                //    }
                                                //    else
                                                //    {
                                                //        mainNicerOutline.effectColor = new Color(mainNicerOutline.effectColor.r, mainNicerOutline.effectColor.g, mainNicerOutline.effectColor.b, mainImage.color.a);
                                                //    }

                                                //    if (someAnimation[mainIndex].al[x].linear)
                                                //    {
                                                //        if (someAnimation[mainIndex].al[x].Fade == 0)
                                                //        {
                                                //            Color clr = mainNicerOutline.effectColor;
                                                //            clr = new Color(0.4f, 0.4f, 0.4f, 0f);
                                                //            clr.a = 0;
                                                //            seq1.Join(DOTween.To(() => mainNicerOutline.effectColor, x => mainNicerOutline.effectColor = x, clr, someAnimation[mainIndex].al[x].durationDot / 2f).OnUpdate(Update).SetEase(Ease.Linear));
                                                //        }
                                                //        else
                                                //        {
                                                //            Color clr = Color.black;
                                                //            clr.a = someAnimation[mainIndex].al[x].Fade;
                                                //            seq1.Join(DOTween.To(() => mainNicerOutline.effectColor, x => mainNicerOutline.effectColor = x, clr, someAnimation[mainIndex].al[x].durationDot / 2f).OnUpdate(Update).SetEase(Ease.Linear));
                                                //        }
                                                //    }
                                                //    else
                                                //    {
                                                //        if (someAnimation[mainIndex].al[x].Fade == 0)
                                                //        {
                                                //            Color clr = mainNicerOutline.effectColor;
                                                //            clr = new Color(0.4f, 0.4f, 0.4f, 0f);
                                                //            clr.a = 0;
                                                //            seq1.Join(DOTween.To(() => mainNicerOutline.effectColor, x => mainNicerOutline.effectColor = x, clr, someAnimation[mainIndex].al[x].durationDot / 2f).OnUpdate(Update).SetEase(someAnimation[mainIndex].al[x].ease));
                                                //        }
                                                //        else
                                                //        {
                                                //            Color clr = Color.black;
                                                //            clr.a = someAnimation[mainIndex].al[x].Fade;
                                                //            seq1.Join(DOTween.To(() => mainNicerOutline.effectColor, x => mainNicerOutline.effectColor = x, clr, someAnimation[mainIndex].al[x].durationDot / 2f).OnUpdate(Update).SetEase(someAnimation[mainIndex].al[x].ease));
                                                //        }
                                                //    }
                                                //}
                                                if (mainCanvasGroup != null)
                                                {
                                                    if (someAnimation[mainIndex].al[x].linear)
                                                    {
                                                        seq1.Join(mainCanvasGroup.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                    }
                                                    else
                                                    {
                                                        seq1.Join(mainCanvasGroup.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                    }
                                                }
                                                else
                                                {
                                                    if (someAnimation[mainIndex].al[x].linear)
                                                    {
                                                        seq1.Join(mainImage.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                    }
                                                    else
                                                    {
                                                        seq1.Join(mainImage.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (mainCanvasGroup != null)
                                                {
                                                    if (someAnimation[mainIndex].al[x].linear)
                                                    {
                                                        seq1.Join(mainCanvasGroup.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                    }
                                                    else
                                                    {
                                                        seq1.Join(mainCanvasGroup.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                    }
                                                }
                                                //if (mainSkeletonGraphic != null)
                                                //{
                                                //    if (someAnimation[mainIndex].al[x].linear)
                                                //    {
                                                //        seq1.Join(mainSkeletonGraphic.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                //    }
                                                //    else
                                                //    {
                                                //        seq1.Join(mainSkeletonGraphic.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                //    }
                                                //}
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (mainText != null)
                                        {
                                            if (someAnimation[mainIndex].al[x].color)
                                            {
                                                if (someAnimation[mainIndex].al[x].linear)
                                                {
                                                    seq1.Join(mainText.DOColor(someAnimation[mainIndex].al[x].clr, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                }
                                                else
                                                {
                                                    seq1.Join(mainText.DOColor(someAnimation[mainIndex].al[x].clr, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                }
                                            }
                                        }
                                        if (mainImage != null)
                                        {
                                            if (someAnimation[mainIndex].al[x].color)
                                            {
                                                if (someAnimation[mainIndex].al[x].linear)
                                                {
                                                    seq1.Join(mainImage.DOColor(someAnimation[mainIndex].al[x].clr, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                }
                                                else
                                                {
                                                    seq1.Join(mainImage.DOColor(someAnimation[mainIndex].al[x].clr, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                }
                                            }
                                        }
                                    }
                                    if (mainImage != null)
                                    {
                                        if (mainImage.type == Image.Type.Filled)
                                        {
                                            if (someAnimation[mainIndex].al[x].doFillAmount)
                                            {
                                                if (someAnimation[mainIndex].al[x].linear)
                                                {
                                                    seq1.Join(mainImage.DOFillAmount(someAnimation[mainIndex].al[x].fillAmount, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                }
                                                else
                                                {
                                                    seq1.Join(mainImage.DOFillAmount(someAnimation[mainIndex].al[x].fillAmount, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                                }
                                            }
                                        }
                                    }
                                    if (!someAnimation[mainIndex].al[x].offScale)
                                    {
                                        if (someAnimation[mainIndex].al[x].linear)
                                        {
                                            seq1.Join(rect.DOScale(someAnimation[mainIndex].al[x].scale, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                        }
                                        else
                                        {
                                            seq1.Join(rect.DOScale(someAnimation[mainIndex].al[x].scale, someAnimation[mainIndex].al[x].durationDot).SetEase(someAnimation[mainIndex].al[x].ease));
                                        }
                                    }
                                    seq1.OnComplete(() => Complete());
                                    if (someAnimation[mainIndex].al[x].offEventTrigger)
                                    {
                                        EventOff();
                                    }
                                    if (someAnimation[mainIndex].al[x].onEventTrigger)
                                    {
                                        Invoke("EventOn", someAnimation[mainIndex].al[x].afterTime);
                                    }
                                }
                                //Dotween for SpriteRenderer
                                if (whatType == "SpriteRenderer")
                                {
                                    seq1 = DOTween.Sequence();
                                    seq1.AppendInterval(someAnimation[mainIndex].al[x].intervalTime);
                                    seq1.AppendInterval(0);
                                    //seq1.Append(transform.DOScale(transform.localScale, 0.01f));
                                    if (someAnimation[mainIndex].al[x].enableJumpAnchorPos)
                                    {
                                        seq1.Append(transform.DOJump(someAnimation[mainIndex].al[x].jumpPosition, someAnimation[mainIndex].al[x].jumpPower, 1, someAnimation[mainIndex].al[x].durationDot));
                                    }
                                    if (someAnimation[mainIndex].al[x].offPosition == false)
                                    {
                                        if (someAnimation[mainIndex].al[x].addPosition)
                                        {
                                            Vector3 anotherPos = new Vector3(transform.localPosition.x, transform.localPosition.y, 0) + someAnimation[mainIndex].al[x].position;

                                            if (someAnimation[mainIndex].al[x].linear)
                                            {
                                                seq1.Append(transform.DOLocalMove(anotherPos, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                            }
                                            else
                                            {
                                                seq1.Append(transform.DOLocalMove(anotherPos, someAnimation[mainIndex].al[x].durationDot));
                                            }
                                        }
                                        else
                                        {
                                            if (someAnimation[mainIndex].al[x].enableJumpAnchorPos == false)
                                            {
                                                if (someAnimation[mainIndex].al[x].linear)
                                                {
                                                    seq1.Append(transform.DOLocalMove(someAnimation[mainIndex].al[x].position, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                                }
                                                else
                                                {
                                                    seq1.Append(transform.DOLocalMove(someAnimation[mainIndex].al[x].position, someAnimation[mainIndex].al[x].durationDot));
                                                }
                                            }
                                        }
                                    }
                                    if (someAnimation[mainIndex].al[x].offRotation == false)
                                    {
                                        if (someAnimation[mainIndex].al[x].Simple)
                                        {
                                            if (someAnimation[mainIndex].al[x].linear)
                                            {
                                                seq1.Join(transform.DORotate(someAnimation[mainIndex].al[x].rotation, someAnimation[mainIndex].al[x].durationDot, RotateMode.Fast).SetEase(Ease.Linear));
                                            }
                                            else
                                            {
                                                seq1.Join(transform.DORotate(someAnimation[mainIndex].al[x].rotation, someAnimation[mainIndex].al[x].durationDot, RotateMode.Fast));
                                            }
                                        }
                                        if (someAnimation[mainIndex].al[x].Beyond360)
                                        {
                                            if (someAnimation[mainIndex].al[x].linear)
                                            {
                                                seq1.Join(transform.DORotate(someAnimation[mainIndex].al[x].rotation, someAnimation[mainIndex].al[x].durationDot, RotateMode.FastBeyond360).SetEase(Ease.Linear));
                                            }
                                            else
                                            {
                                                seq1.Join(transform.DORotate(someAnimation[mainIndex].al[x].rotation, someAnimation[mainIndex].al[x].durationDot, RotateMode.FastBeyond360));
                                            }
                                        }
                                    }
                                    if (!someAnimation[mainIndex].al[x].offFade)
                                    {
                                        if (mainSpriteRenderer != null)
                                        {
                                            if (someAnimation[mainIndex].al[x].linear)
                                            {
                                                seq1.Join(mainSpriteRenderer.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot).SetEase(Ease.Linear));
                                            }
                                            else
                                            {
                                                seq1.Join(mainSpriteRenderer.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot));
                                            }
                                        }
                                    }
                                    if (!someAnimation[mainIndex].al[x].offScale)
                                    {
                                        seq1.Join(transform.DOScale(someAnimation[mainIndex].al[x].scale, someAnimation[mainIndex].al[x].durationDot));
                                    }
                                    seq1.OnComplete(() => Complete());
                                    if (someAnimation[mainIndex].al[x].offEventTrigger)
                                    {
                                        EventOff();
                                    }
                                    if (someAnimation[mainIndex].al[x].onEventTrigger)
                                    {
                                        Invoke("EventOn", someAnimation[mainIndex].al[x].afterTime);
                                    }
                                }
                                //Dotween for MeshRenderer
                                if (whatType == "MeshRenderer")
                                {
                                    seq1 = DOTween.Sequence();
                                    seq1.AppendInterval(someAnimation[mainIndex].al[x].intervalTime);
                                    seq1.AppendInterval(0);
                                    //seq1.Append(transform.DOScale(transform.localScale, 0.01f));
                                    if (someAnimation[mainIndex].al[x].enableJumpAnchorPos)
                                    {
                                        seq1.Append(transform.DOJump(someAnimation[mainIndex].al[x].jumpPosition, someAnimation[mainIndex].al[x].jumpPower, 1, someAnimation[mainIndex].al[x].durationDot));
                                    }
                                    if (someAnimation[mainIndex].al[x].offPosition == false)
                                    {
                                        if (someAnimation[mainIndex].al[x].addPosition)
                                        {
                                            Vector3 anotherPos = new Vector3(transform.localPosition.x, transform.localPosition.y, 0) + someAnimation[mainIndex].al[x].position;
                                            seq1.Append(transform.DOLocalMove(anotherPos, someAnimation[mainIndex].al[x].durationDot));
                                        }
                                        else
                                        {
                                            if (someAnimation[mainIndex].al[x].enableJumpAnchorPos == false)
                                            {
                                                seq1.Append(transform.DOLocalMove(someAnimation[mainIndex].al[x].position, someAnimation[mainIndex].al[x].durationDot));
                                            }
                                        }
                                    }
                                    if (someAnimation[mainIndex].al[x].offRotation == false)
                                    {
                                        if (someAnimation[mainIndex].al[x].Simple)
                                        {
                                            seq1.Join(transform.DORotate(someAnimation[mainIndex].al[x].rotation, someAnimation[mainIndex].al[x].durationDot, RotateMode.Fast));
                                        }
                                        if (someAnimation[mainIndex].al[x].Beyond360)
                                        {
                                            seq1.Join(transform.DORotate(someAnimation[mainIndex].al[x].rotation, someAnimation[mainIndex].al[x].durationDot, RotateMode.FastBeyond360));
                                        }
                                    }
                                    if (!someAnimation[mainIndex].al[x].offFade)
                                    {
                                        if (mainMeshRenderer != null)
                                        {
                                            //if (mainSkeleton != null)
                                            //{
                                            //    Shader shader;
                                            //    shader = Shader.Find("Sprites/Default");
                                            //    mainMeshRenderer.sharedMaterial.shader = shader;
                                            //    seq1.Join(mainMeshRenderer.sharedMaterial.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot));
                                            //}
                                            //else
                                            //{
                                            //    seq1.Join(mainMeshRenderer.material.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot));
                                            //}

                                            seq1.Join(mainMeshRenderer.material.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot));
                                        }
                                    }
                                    if (!someAnimation[mainIndex].al[x].offScale)
                                    {
                                        seq1.Join(transform.DOScale(someAnimation[mainIndex].al[x].scale, someAnimation[mainIndex].al[x].durationDot));
                                    }
                                    seq1.OnComplete(() => Complete());
                                    if (someAnimation[mainIndex].al[x].offEventTrigger)
                                    {
                                        EventOff();
                                    }
                                    if (someAnimation[mainIndex].al[x].onEventTrigger)
                                    {
                                        Invoke("EventOn", someAnimation[mainIndex].al[x].afterTime);
                                    }
                                }
                                //Dotween for Renderer
                                if (whatType == "Renderer")
                                {
                                    seq1 = DOTween.Sequence();
                                    seq1.AppendInterval(someAnimation[mainIndex].al[x].intervalTime);
                                    seq1.AppendInterval(0);
                                    //seq1.Append(transform.DOScale(transform.localScale, 0.01f));
                                    if (someAnimation[mainIndex].al[x].enableJumpAnchorPos)
                                    {
                                        seq1.Append(transform.DOJump(someAnimation[mainIndex].al[x].jumpPosition, someAnimation[mainIndex].al[x].jumpPower, 1, someAnimation[mainIndex].al[x].durationDot));
                                    }
                                    if (someAnimation[mainIndex].al[x].offPosition == false)
                                    {
                                        if (someAnimation[mainIndex].al[x].addPosition)
                                        {
                                            Vector3 anotherPos = new Vector3(transform.localPosition.x, transform.localPosition.y, 0) + someAnimation[mainIndex].al[x].position;
                                            seq1.Append(transform.DOLocalMove(anotherPos, someAnimation[mainIndex].al[x].durationDot));
                                        }
                                        else
                                        {
                                            if (someAnimation[mainIndex].al[x].enableJumpAnchorPos == false)
                                            {
                                                seq1.Append(transform.DOLocalMove(someAnimation[mainIndex].al[x].position, someAnimation[mainIndex].al[x].durationDot));
                                            }
                                        }
                                    }
                                    if (someAnimation[mainIndex].al[x].offRotation == false)
                                    {
                                        if (someAnimation[mainIndex].al[x].Simple)
                                        {
                                            seq1.Join(transform.DORotate(someAnimation[mainIndex].al[x].rotation, someAnimation[mainIndex].al[x].durationDot, RotateMode.Fast));
                                        }
                                        if (someAnimation[mainIndex].al[x].Beyond360)
                                        {
                                            seq1.Join(transform.DORotate(someAnimation[mainIndex].al[x].rotation, someAnimation[mainIndex].al[x].durationDot, RotateMode.FastBeyond360));
                                        }
                                    }
                                    if (!someAnimation[mainIndex].al[x].offFade)
                                    {
                                        if (mainRenderer != null)
                                        {
                                            seq1.Join(mainRenderer.material.DOFade(someAnimation[mainIndex].al[x].Fade, someAnimation[mainIndex].al[x].durationDot));
                                        }
                                    }
                                    if (!someAnimation[mainIndex].al[x].offScale)
                                    {
                                        seq1.Join(transform.DOScale(someAnimation[mainIndex].al[x].scale, someAnimation[mainIndex].al[x].durationDot));
                                    }
                                    seq1.OnComplete(() => Complete());
                                    if (someAnimation[mainIndex].al[x].offEventTrigger)
                                    {
                                        EventOff();
                                    }
                                    if (someAnimation[mainIndex].al[x].onEventTrigger)
                                    {
                                        Invoke("EventOn", someAnimation[mainIndex].al[x].afterTime);
                                    }
                                }
                            }
                        }
                        if (someAnimation[mainIndex].al[x].checkAnim)
                        {
                            if (someAnimation[mainIndex].al[x].durationAnim == 0)
                            {
                                someAnimation[mainIndex].al[x].eventsAnim.Invoke();
                            }
                            someAnimation[mainIndex].al[x].gameObjectAnim.Play();
                            Invoke("Complete", someAnimation[mainIndex].al[x].durationAnim);
                            if (someAnimation[mainIndex].al[x].offEventTriggerAnim)
                            {
                                EventOff();
                            }
                            if (someAnimation[mainIndex].al[x].onEventTriggerAnim)
                            {
                                Invoke("EventOn", someAnimation[mainIndex].al[x].afterTime);
                            }
                        }
                        if (someAnimation[mainIndex].al[x].checkFrame)
                        {
                            Invoke("frameWait", someAnimation[mainIndex].al[x].waitTime);
                            if (someAnimation[mainIndex].al[x].offEventTriggerFrame)
                            {
                                EventOff();
                            }
                            if (someAnimation[mainIndex].al[x].onEventTriggerFrame)
                            {
                                Invoke("EventOn", someAnimation[mainIndex].al[x].afterTime);
                            }
                        }
                        if (someAnimation[mainIndex].al[x].checkPart)
                        {
                            if (someAnimation[mainIndex].al[x].durationPart == 0)
                            {
                                someAnimation[mainIndex].al[x].eventsPart.Invoke();
                            }
                            someAnimation[mainIndex].al[x].particle.Play();
                            Invoke("Complete", someAnimation[mainIndex].al[x].durationPart);
                            if (someAnimation[mainIndex].al[x].offEventTriggerPart)
                            {
                                EventOff();
                            }
                            if (someAnimation[mainIndex].al[x].onEventTriggerPart)
                            {
                                Invoke("EventOn", someAnimation[mainIndex].al[x].afterTime);
                            }
                            Debug.Log("paeticle");
                        }
                        if (someAnimation[mainIndex].al[x].checkSpine)
                        {
                            if (someAnimation[mainIndex].al[x].durationSpine == 0)
                            {
                                someAnimation[mainIndex].al[x].eventsSpine.Invoke();
                            }
                            Invoke("Spine", someAnimation[mainIndex].al[x].waitSpineTime);
                            if (someAnimation[mainIndex].al[x].offEventTriggerSpine)
                            {
                                EventOff();
                            }
                            if (someAnimation[mainIndex].al[x].onEventTriggerSpine)
                            {
                                Invoke("EventOn", someAnimation[mainIndex].al[x].afterTime);
                            }
                        }
                    }
                }
            }
        }
    }

    void ValueChanged()
    {
        if(someAnimation.Count > 0)
        {
            someAnimation[someAnimation.Count - 1].mono = GetComponent<MonoBehaviour>();
        }
    }

    public void frameWait()
    {
        checkFrameSecond = true;
    }

    public void Spine()
    {
        //if (someAnimation[mainIndex].al[x].spriteSkeleton)
        //{
        //    someAnimation[mainIndex].al[x].skeletonSprite.AnimationState.SetAnimation(0, someAnimation[mainIndex].al[x].animationState, true);
        //}
        //else
        //{
        //    if(someAnimation[mainIndex].al[x].skeleton != null)
        //    someAnimation[mainIndex].al[x].skeleton.AnimationState.SetAnimation(0, someAnimation[mainIndex].al[x].animationState, true);
        //}
        Invoke("Complete", someAnimation[mainIndex].al[x].durationSpine);
    }

    public void EventOff()
    {
        if(Event != null)
        {
            Event.enabled = false;
        }
    }

    public void EventOn()
    {
        if (Event != null)
        {
            Event.enabled = true;
        }
    }

    public void Complete()
    {
        x++;
        int invoke = x - 1;

        if (x < someAnimation[mainIndex].al.Count)
        {
            check = true;
            once = true;
            index = 0;
        }
        else//End
        {
            someAnimation[mainIndex].isEnd = true;
            someAnimation[mainIndex].Play = false;

            if (!someAnimation[mainIndex].infinityStartPlay)
            {
                someAnimation[mainIndex].playOnStart = false;
            }
            if (mainEventTrigger != null)
            {
                mainEventTrigger.enabled = true;
            }
            if (mainBoxCollider != null)
            {
                mainBoxCollider.enabled = true;
            }
            if (someAnimation[mainIndex].objectsToActive.Count > 0)
            {
                for (int r = 0; r < someAnimation[mainIndex].objectsToActive.Count; r++)
                {
                    someAnimation[mainIndex].objectsToActive[r].SetActive(true);
                }
            }
            if (someAnimation[mainIndex].objectsToDeactive.Count > 0)
            {
                for (int r = 0; r < someAnimation[mainIndex].objectsToDeactive.Count; r++)
                {
                    someAnimation[mainIndex].objectsToDeactive[r].SetActive(false);
                }
            }
            if (someAnimation[mainIndex].destroyOnComplete)
            {
                Destroy(gameObject);
            }
            if (someAnimation[mainIndex].offOnComplete)
            {
                gameObject.SetActive(false);
            }
            //if (mainSkeleton != null)
            //{
            //    Shader shader;
            //    shader = Shader.Find("Sprites/Default");
            //    mainMeshRenderer.sharedMaterial.shader = shader;
            //    mainMeshRenderer.sharedMaterial.color = Color.white;
            //}
            if(mainRenderer != null)
            {
                //Material material = new Material(Shader.Find("Particles/Standard Unlit"));
                //mainRenderer.material = material;
                //mainRenderer.material.color = Color.white;

                //Color clr = Color.white;
                //clr.a = 1;
                //mainRenderer.material.color = clr;
            }

            check = true;
            once = true;
            x = 0;
            index = 0;
            checkFrameSecond = false;
            playing = false;

            if (someAnimation[mainIndex].loop)
            {
                someAnimation[mainIndex].Play = true;
            }
        }

        if (someAnimation[mainIndex].al[invoke].checkAnim)
        {
            someAnimation[mainIndex].al[invoke].eventsAnim.Invoke();
        }
        if (someAnimation[mainIndex].al[invoke].checkFrame)
        {
            someAnimation[mainIndex].al[invoke].eventsFrame.Invoke();
        }
        if (someAnimation[mainIndex].al[invoke].checkPart)
        {
            someAnimation[mainIndex].al[invoke].eventsPart.Invoke();
        }
        if (someAnimation[mainIndex].al[invoke].checkSpine)
        {
            someAnimation[mainIndex].al[invoke].eventsSpine.Invoke();
        }
        if (someAnimation[mainIndex].al[invoke].checkDot)
        {
            someAnimation[mainIndex].al[invoke].Events.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        coll = collision.gameObject;
        for (int q = 0; q < someAnimation.Count; q++)
        {
            if (collision.tag == someAnimation[q].name)
            {
                mainIndex = q;
                someAnimation[mainIndex].Play = true;
                if (mainBoxCollider != null)
                {
                    mainBoxCollider.enabled = false;
                }
                if (mainEventTrigger != null)
                {
                    mainEventTrigger.enabled = false;
                }
                if (someAnimation[q].advanced)
                {
                    if (someAnimation[q].destroyCollidedObj)
                    {
                        Destroy(coll, someAnimation[q].destroyTime);
                    }
                    if (someAnimation[q].offCollidedObj)
                    {
                        coll.SetActive(false);
                    }
                    if (someAnimation[q].offCollidedBoxCollider)
                    {
                        coll.GetComponent<BoxCollider2D>().enabled = false;
                    }
                }
                break;
            }
        }
    }

    public void CallByName(string indexName)
    {
        for (int q = 0; q < someAnimation.Count; q++)
        {
            if (indexName == someAnimation[q].name && !someAnimation[q].playOnStart)
            {
                if (!gameObject.activeSelf)
                {
                    if (someAnimation[q].activeWhenCall)
                    {
                        gameObject.SetActive(true);
                    }
                }

                mainIndex = q;
                if (someAnimation[mainIndex].al.Count > 0)
                {
                    seq1.Kill();//In some case better to disable this, but this need for pointerdown and pointerup events

                    index = 0;
                    x = 0;
                    once = true;
                    check = true;
                    checkFrameSecond = false;

                    someAnimation[mainIndex].Play = true;
                }
                else
                {
                    if (someAnimation[mainIndex].objectsToActive.Count > 0)
                    {
                        for (int r = 0; r < someAnimation[mainIndex].objectsToActive.Count; r++)
                        {
                            someAnimation[mainIndex].objectsToActive[r].SetActive(true);
                        }
                    }
                    if (someAnimation[mainIndex].objectsToDeactive.Count > 0)
                    {
                        for (int r = 0; r < someAnimation[mainIndex].objectsToDeactive.Count; r++)
                        {
                            someAnimation[mainIndex].objectsToDeactive[r].SetActive(false);
                        }
                    }
                    if (someAnimation[mainIndex].destroyOnComplete)
                    {
                        Destroy(gameObject);
                    }
                    if (someAnimation[mainIndex].offOnComplete)
                    {
                        gameObject.SetActive(false);
                    }
                }
                break;
            }
        }
    }

    public new void GetType()
    {
        rect = gameObject.GetComponent<RectTransform>();

        if (gameObject.GetComponent<MeshRenderer>() != null)
        {
            whatType = "MeshRenderer";
            mainMeshRenderer = gameObject.GetComponent<MeshRenderer>();
        }
        if (gameObject.GetComponent<Renderer>() != null)
        {
            whatType = "Renderer";
            mainRenderer = gameObject.GetComponent<Renderer>();
        }
        if (gameObject.GetComponent<SpriteRenderer>() != null)
        {
            whatType = "SpriteRenderer";
            mainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        }
        if (gameObject.GetComponent<CanvasRenderer>() != null)
        {
            whatType = "CanvasRenderer";
            mainCanvasRenderer = gameObject.GetComponent<CanvasRenderer>();
        }
        //if (gameObject.GetComponent<SkeletonAnimation>() != null)
        //{
        //    mainSkeleton = gameObject.GetComponent<SkeletonAnimation>();
        //}
        if (gameObject.GetComponent<Text>() != null)
        {
            mainText = gameObject.GetComponent<Text>();
        }
        if (gameObject.GetComponent<Image>() != null)
        {
            mainImage = gameObject.GetComponent<Image>();
        }
        if (gameObject.GetComponent<CanvasGroup>() != null)
        {
            mainCanvasGroup = gameObject.GetComponent<CanvasGroup>();
        }
        if (gameObject.GetComponent<Outline>() != null)
        {
            mainOutline = gameObject.GetComponent<Outline>();
        }
        //if (gameObject.GetComponent<NicerOutline>() != null)
        //{
        //    mainNicerOutline = gameObject.GetComponent<NicerOutline>();
        //}
        //if (gameObject.GetComponent<BestFitOutline>() != null)
        //{
        //    mainBestFitOutline = gameObject.GetComponent<BestFitOutline>();
        //}
        if (gameObject.GetComponent<EventTrigger>() != null)
        {
            mainEventTrigger = gameObject.GetComponent<EventTrigger>();
        }
        if (gameObject.GetComponent<BoxCollider2D>() != null)
        {
            mainBoxCollider = gameObject.GetComponent<BoxCollider2D>();
        }
        //if (gameObject.GetComponent<SkeletonGraphic>() != null)
        //{
        //    mainSkeletonGraphic = gameObject.GetComponent<SkeletonGraphic>();
        //}

        componentGetted = true;
    }

    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            Kill();

            //if (mainSkeleton != null)
            //{
            //    Shader shader;
            //    shader = Shader.Find("Sprites/Default");
            //    mainMeshRenderer.sharedMaterial.shader = shader;
            //    mainMeshRenderer.sharedMaterial.color = Color.white;
            //}
            if(mainRenderer != null)
            {
                //Material material = new Material(Shader.Find("Particles/Standard Unlit"));
                //mainRenderer.material = material;
                //mainRenderer.material.color = Color.white;

                //Color clr = Color.white;
                //clr.a = 1;
                //mainRenderer.material.color = clr;

                startRendererClr.a = startRendererAlpha;
                mainRenderer.material.color = startRendererClr;
            }
        }
    }

    public void Kill()
    {
        if (Application.isPlaying)
        {
            seq1.Kill();

            index = 0;
            x = 0;
            once = true;
            check = true;
            checkFrameSecond = false;

            if(someAnimation.Count > 0)
            {
                someAnimation[mainIndex].Play = false;
            }
            else
            {
                Debug.Log($"Empty Animation Pro in: {gameObject.name}", gameObject);
            }
        }
    }

    //Needed Methods
    public void DestroyObject(GameObject dest)
    {
        Destroy(dest);
    }

    public void SetAsFirstSibling()
    {
        transform.SetAsFirstSibling();
    }

    public void SetAsLastSibling()
    {
        transform.SetAsLastSibling();
    }

    public void SetSiblingIndex(int index)
    {
        transform.SetSiblingIndex(index);
    }
}

[System.Serializable]
public class someAnimation
{
    [HideInInspector]
    public MonoBehaviour mono;
    [PropertySpace(SpaceBefore = 15)]
    [HideIf("playOnStart")]
    [HideLabel]
    public string name = "Animation Name";
    public bool playOnStart;
    [ShowIf("playOnStart")]
    public bool infinityStartPlay;
    [HideIf("playOnStart")]
    public bool activeWhenCall;
    public bool destroyOnComplete;
    public bool offOnComplete;
    public bool loop;
    [HideIf("playOnStart")]
    public bool advanced;
    [ShowIf("advanced")]
    [HideIf("playOnStart")]
    [LabelWidth(200)]
    [Indent(3)]
    public bool destroyCollidedObj;
    [ShowIf("destroyCollidedObj")]
    [ShowIf("advanced")]
    [HideIf("playOnStart")]
    [LabelWidth(140)]
    [Indent(3)]
    public float destroyTime;
    [ShowIf("advanced")]
    [HideIf("playOnStart")]
    [LabelWidth(200)]
    [Indent(3)]
    public bool offCollidedObj;
    [ShowIf("advanced")]
    [HideIf("playOnStart")]
    [LabelWidth(200)]
    [Indent(3)]
    public bool offCollidedBoxCollider;

    //[HideInInspector]
    [Sirenix.OdinInspector.ReadOnly]
    [ShowIf("advanced")]
    [HideIf("playOnStart")]
    [LabelWidth(200)]
    [Indent(3)]
    public bool Play;

    [OnValueChanged("ValueChanged")]
    public List<all> al;
    public List<GameObject> objectsToActive;
    [PropertySpace(SpaceBefore = 0, SpaceAfter = 15)]
    public List<GameObject> objectsToDeactive;
    [HideInInspector]
    public bool isEnd;

    void ValueChanged()
    {
        if(al.Count > 0)
        {
            al[al.Count - 1].mono = mono;
        }
    }
}


[System.Serializable]
public class all
{
    float defaultFade;
    Vector3 defaultPosition, defaultRotation;
    Vector2 defaultScale;
    [HideInInspector]
    public MonoBehaviour mono;
    [HideInInspector]
    public bool record = false;
    [HideInInspector]
    public bool checkDot;
    [Title("DoTween", titleAlignment: TitleAlignments.Centered)]
    [ShowIf("checkDot")]
    public bool hide;
    [HorizontalGroup("raim", LabelWidth = 110)]
    [HideIf("onEventTrigger")]
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public bool offEventTrigger;
    [HorizontalGroup("raim", LabelWidth = 110)]
    [HideIf("offEventTrigger")]
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public bool onEventTrigger;
    [HorizontalGroup("raim", LabelWidth = 110)]
    [ShowIf("onEventTrigger")]
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public float afterTime = 0;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public float intervalTime = 0;
    [ShowIf("checkDot")]
    [ProgressBar(0, 1, r: 1, g: 1, b: 1, Height = 15)]
    [HideIf("hide")]
    [HideIf("offFade")]
    public float Fade = 1;
    [LabelWidth(150)]
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public bool enableJumpAnchorPos;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public bool linear;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public Ease ease;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public bool color;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    [ShowIf("color")]
    public Color clr;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public bool doFillAmount;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    [ShowIf("doFillAmount")]
    [Range(0, 1)]
    public float fillAmount;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public bool offFade;
    [ShowIf("checkDot")]
    [HideIf("addPosition")]
    [HideIf("hide")]
    public bool offPosition;
    [ShowIf("checkDot")]
    [HideIf("offPosition")]
    [HideIf("hide")]
    public bool addPosition;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public bool offRotation;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public bool offScale;
    [ShowIf("enableJumpAnchorPos")]
    [HideIf("hide")]
    public Vector3 jumpPosition;
    [ShowIf("enableJumpAnchorPos")]
    [HideIf("hide")]
    public float jumpPower;
    [ShowIf("checkDot")]
    [HideIf("enableJumpAnchorPos")]
    [HideIf("offPosition")]
    [HideIf("hide")]
    public Vector3 position;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    [HideIf("offRotation")]
    public Vector3 rotation;
    [ShowIf("checkDot")]
    [HorizontalGroup("rotate", marginLeft: 120)]
    [LabelWidth(50)]
    [HideIf("Beyond360")]
    [HideIf("hide")]
    public bool Simple = true;
    [ShowIf("checkDot")]
    [HorizontalGroup("rotate")]
    [LabelWidth(80)]
    [HideIf("Simple")]
    [HideIf("hide")]
    public bool Beyond360;
    [ShowIf("checkDot")]
    [HideIf("offScale")]
    [HideIf("hide")]
    public Vector2 scale;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public float durationDot = 1;
    [ShowIf("checkDot")]
    [HideIf("hide")]
    public UnityEvent Events;
    [ShowIf("checkDot")]
    [HideIf("record")]
    [HideIf("hide")]
    [Button, GUIColor(0, 1, 0)]
    void Record()
    {
        //if (mono.GetComponent<Record>() == null)
        //{
        //    record = true;
        //    var rec = mono.gameObject.AddComponent<Record>();
        //    rec.obj = mono.gameObject;
        //    if (mono.gameObject.GetComponent<SpriteRenderer>() != null)
        //    {
        //        defaultFade = mono.gameObject.GetComponent<SpriteRenderer>().color.a;
        //    }
        //    if (mono.gameObject.GetComponent<CanvasRenderer>() != null)
        //    {
        //        if (mono.gameObject.GetComponent<Image>() != null)
        //        {
        //            if (mono.gameObject.GetComponent<CanvasGroup>() != null)
        //            {
        //                defaultFade = mono.gameObject.GetComponent<CanvasGroup>().alpha;
        //            }
        //            else
        //            {
        //                defaultFade = mono.gameObject.GetComponent<Image>().color.a;
        //            }
        //        }
        //    }
        //    defaultPosition = mono.gameObject.transform.position;
        //    defaultRotation = mono.gameObject.transform.eulerAngles;
        //    defaultScale = mono.gameObject.transform.localScale;
        //}
        //else
        //{
        //    Debug.Log("Another Animation Already Recording!");
        //}

    }
    [ShowIf("checkDot")]
    [ShowIf("record")]
    [HideIf("hide")]
    [Button, GUIColor(1, 0.3f, 0.3f)]
    void Pause()
    {
        record = false;
        //UnityEngine.Object.DestroyImmediate(mono.gameObject.GetComponent<Record>());
    }

    [ShowIf("checkDot")]
    [HideIf("hide")]
    [Button, GUIColor(0, 1f, 1f)]
    void Reset()
    {
        Pause();
        if (mono.gameObject.GetComponent<SpriteRenderer>() != null)
        {
            mono.gameObject.GetComponent<SpriteRenderer>().color = new Color(mono.gameObject.GetComponent<SpriteRenderer>().color.r, mono.gameObject.GetComponent<SpriteRenderer>().color.g, mono.gameObject.GetComponent<SpriteRenderer>().color.b, defaultFade);
        }
        if (mono.gameObject.GetComponent<CanvasRenderer>() != null)
        {
            if (mono.gameObject.GetComponent<Image>() != null)
            {
                if (mono.gameObject.GetComponent<CanvasGroup>() != null)
                {
                    mono.gameObject.GetComponent<CanvasGroup>().alpha = defaultFade;
                }
                else
                {
                    mono.gameObject.GetComponent<Image>().color = new Color(mono.gameObject.GetComponent<Image>().color.r, mono.gameObject.GetComponent<Image>().color.g, mono.gameObject.GetComponent<Image>().color.b, defaultFade);
                }
            }
        }
        mono.gameObject.transform.position = defaultPosition;
        mono.gameObject.transform.eulerAngles = defaultRotation;
        mono.gameObject.transform.localScale = defaultScale;
    }

    [HideInInspector]
    public bool checkAnim;
    [Title("Animation", titleAlignment: TitleAlignments.Centered)]
    [ShowIf("checkAnim")]
    public Animation gameObjectAnim;
    [ShowIf("checkAnim")]
    public float durationAnim = 1;
    [HorizontalGroup("raim1", LabelWidth = 110)]
    [HideIf("onEventTriggerAnim")]
    [ShowIf("checkAnim")]
    public bool offEventTriggerAnim;
    [HorizontalGroup("raim1", LabelWidth = 110)]
    [HideIf("offEventTriggerAnim")]
    [ShowIf("checkAnim")]
    public bool onEventTriggerAnim;
    [ShowIf("checkAnim")]
    public UnityEvent eventsAnim;

    [HideInInspector]
    public bool checkFrame;
    [Title("Frame", titleAlignment: TitleAlignments.Centered)]
    [ShowIf("checkFrame")]
    public GameObject[] frames;
    [ShowIf("checkFrame")]
    public float waitTime;
    [ShowIf("checkFrame")]
    public float eachFrameDuration;
    [ShowIf("checkFrame")]
    public bool offPreviousFrame;
    [ShowIf("checkFrame")]
    public bool offAllFrame;
    [HorizontalGroup("raim2", LabelWidth = 110)]
    [HideIf("onEventTriggerFrame")]
    [ShowIf("checkFrame")]
    public bool offEventTriggerFrame;
    [HorizontalGroup("raim2", LabelWidth = 110)]
    [HideIf("offEventTriggerFrame")]
    [ShowIf("checkFrame")]
    public bool onEventTriggerFrame;
    [ShowIf("checkFrame")]
    public UnityEvent eventsFrame;

    [HideInInspector]
    public bool checkPart;
    [Title("Particle", titleAlignment: TitleAlignments.Centered)]
    [ShowIf("checkPart")]
    public ParticleSystem particle;
    [ShowIf("checkPart")]
    public float durationPart = 1;
    [HorizontalGroup("raim3", LabelWidth = 110)]
    [HideIf("onEventTriggerPart")]
    [ShowIf("checkPart")]
    public bool offEventTriggerPart;
    [HorizontalGroup("raim3", LabelWidth = 110)]
    [HideIf("offEventTriggerPart")]
    [ShowIf("checkPart")]
    public bool onEventTriggerPart;
    [ShowIf("checkPart")]
    public UnityEvent eventsPart;

    [HideInInspector]
    public bool checkSpine;
    [Title("Spine", titleAlignment: TitleAlignments.Centered)]
    [ShowIf("checkSpine")]
    public float waitSpineTime;
    [ShowIf("checkSpine")]
    public bool spriteSkeleton;
    //[ShowIf("checkSpine")]
    //[ShowIf("spriteSkeleton")]
    //public SkeletonAnimation skeletonSprite;
    //[ShowIf("checkSpine")]
    //[HideIf("spriteSkeleton")]
    //public SkeletonGraphic skeleton;
    [ShowIf("checkSpine")]
    public string animationState;
    [ShowIf("checkSpine")]
    public float durationSpine = 1;
    [HorizontalGroup("raim4", LabelWidth = 110)]
    [HideIf("onEventTriggerSpine")]
    [ShowIf("checkSpine")]
    public bool offEventTriggerSpine;
    [HorizontalGroup("raim4", LabelWidth = 110)]
    [HideIf("offEventTriggerSpine")]
    [ShowIf("checkSpine")]
    public bool onEventTriggerSpine;
    [ShowIf("checkSpine")]
    public UnityEvent eventsSpine;

    [HideIf("checkAnim")]
    [HideIf("checkFrame")]
    [HideIf("checkPart")]
    [HideIf("checkDot")]
    [HideIf("checkSpine")]
    [ButtonGroup]
    private void Dotween()
    {
        checkDot = true;
        checkAnim = false;
        checkFrame = false;
        checkPart = false;
        checkSpine = false;
        scale = new Vector2(1, 1);
    }

    [HideIf("checkDot")]
    [HideIf("checkFrame")]
    [HideIf("checkPart")]
    [HideIf("checkAnim")]
    [HideIf("checkSpine")]
    [ButtonGroup]
    private void Anim()
    {
        checkDot = false;
        checkAnim = true;
        checkFrame = false;
        checkPart = false;
        checkSpine = false;
    }

    [HideIf("checkAnim")]
    [HideIf("checkDot")]
    [HideIf("checkPart")]
    [HideIf("checkFrame")]
    [HideIf("checkSpine")]
    [ButtonGroup]
    private void Frame()
    {
        checkDot = false;
        checkAnim = false;
        checkFrame = true;
        checkPart = false;
        checkSpine = false;
    }

    [HideIf("checkAnim")]
    [HideIf("checkFrame")]
    [HideIf("checkDot")]
    [HideIf("checkPart")]
    [HideIf("checkSpine")]
    [ButtonGroup]
    private void Part()
    {
        checkDot = false;
        checkAnim = false;
        checkFrame = false;
        checkPart = true;
        checkSpine = false;
    }
    [HideIf("checkAnim")]
    [HideIf("checkFrame")]
    [HideIf("checkDot")]
    [HideIf("checkPart")]
    [HideIf("checkSpine")]
    [ButtonGroup]
    private void Spine()
    {
        checkDot = false;
        checkAnim = false;
        checkFrame = false;
        checkPart = false;
        checkSpine = true;
    }
    [OnInspectorGUI]
    private void Space2()
    {
        GUILayout.Space(10);
    }
}
}