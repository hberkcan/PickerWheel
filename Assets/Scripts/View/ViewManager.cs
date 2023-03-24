using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyMessagingSystem;

public class ViewManager : MonoBehaviour, IMessagingSubscriber<DestroyInventoryMessage>
{
    public static ViewManager Instance { get; private set; }

    [SerializeField] private View mainView;
    [SerializeField] private View[] views;
    private View currentView;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        MessagingSystem.Instance.Subscribe<DestroyInventoryMessage>(this);
    }

    private void Start() 
    {
        for(int i = 0; i < views.Length; i++)
        {
            View view = views[i];
            view.Init();
            view.Hide();
        }

        if (mainView != null)
            Show(mainView);
    }

    private void OnDisable()
    {
        MessagingSystem.Instance.Unsubscribe<DestroyInventoryMessage>(this);
    }

    public void Show(View view)
    {
        if (currentView != null)
            currentView.Hide();

        view.Show();
        currentView = view;
    }

    public void Show<T>() where T : View
    {
        for(int i = 0; i < views.Length; i++)
        {
            if(views[i] is T)
            {
                Show(views[i]);
            }
        }
    }

    public void OnReceiveMessage(DestroyInventoryMessage message)
    {
        Show<BombView>();
    }
}
