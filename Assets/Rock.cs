using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rock : MonoBehaviour
{
    Rigidbody rocketRB;

    //Audio
    [SerializeField] AudioClip mainEngine;
    AudioSource audioSource;

    //Speed
    [SerializeField] float thrustSpeed;
    [SerializeField] float RotationSpeed;

    void Start()
    {
        rocketRB = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Rotate();
        Thrust();
    }

    // Update is called once per frame
    void Thrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rocketRB.AddRelativeForce(Vector3.up * thrustSpeed);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rocketRB.AddRelativeForce(Vector3.up);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(mainEngine);
            }
        }
       else
        {
            audioSource.Stop();
        }
    }

    void Rotate()
    {
        rocketRB.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * RotationSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.back * RotationSpeed);
        }
        rocketRB.freezeRotation = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag) {
            case "Friendly":
                print("Your Okay");
                break;
            case "Finish":
                print("You Win");
                LoadNextScene();
                break;
            case "Deathly":
                print("YOU Dead");
                SceneManager.LoadScene(0);
                break;
        }
    }

    void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}