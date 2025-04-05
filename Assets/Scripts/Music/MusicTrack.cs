using UnityEngine;

public class MusicTrack : MonoBehaviour
{
    [SerializeField] AudioSource asSynth01;
    [SerializeField] AudioSource asSynth02;
    [SerializeField] AudioSource asPiano01;
    [SerializeField] AudioSource asPiano02;
    bool syth01 = false;
    bool syth02 = false;
    bool piano01 = false;
    bool piano02 = false;


    bool dtSyth01 = false;
    bool dtSyth02 = false;
    bool dtPiano01 = false;
    bool dtPiano02 = false;

    float tiempo = 0;
    float tiempoMaximo = 0;

//--------------------------------------------------------
    void Start()
    {
        tiempoMaximo = asSynth01.clip.length;
        asSynth01.Play();
        asSynth01.mute = true;
        asSynth02.Play();
        asSynth02.mute = true;
        asPiano01.Play();
        asPiano01.mute = true;
        asPiano02.Play();
        asPiano02.mute = true;
    }

    void Update()
    {
        tiempo += Time.deltaTime;
        Debug.Log("Tiempo: " + tiempo);

        if (tiempo > tiempoMaximo)
        {
            tiempo = 0;
        }

        if(Input.GetKeyDown(KeyCode.U))
        {
            dtSyth01 = true;
            //syth01 = !syth01;
            //ActivarSyth01();
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            dtSyth02 = true;
            //syth02 = !syth02;
            //ActivarSyth02();
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            dtPiano01 = true;
            //piano01 = !piano01;
            //ActivarPiano01();
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            dtPiano02 = true;
            //piano02 = !piano02;
            //ActivarPiano02();
        }

        ActivarMusica();
    }

//--------------------------------------------------------
    void ActivarMusica()
    {
        if( tiempo == 0)
        {
            if(dtSyth01)
            {
                syth01 = !syth01;
                ActivarSyth01();
                dtSyth01 = false;
            }

            if(dtSyth02)
            {
                syth02 = !syth02;
                ActivarSyth02();
                dtSyth02 = false;
            }

            if(dtPiano01)
            {
                piano01 = !piano01;
                ActivarPiano01();
                dtPiano01 = false;
            }

            if(dtPiano02)
            {
                piano02 = !piano02;
                ActivarPiano02();
                dtPiano02 = false;
            }
        }
    }





    void ActivarSyth01()
    {
        if(syth01)
        {
            //asSynth01.mute = false;
            asSynth01.Play();
        }
        else
        {
            //asSynth01.mute = true;
            asSynth01.Stop();
        }
    }
    void ActivarSyth02()
    {
        if(syth02)
        {
            //asSynth02.mute = false;
            asSynth02.Play();
        }
        else
        {
            //asSynth02.mute = true;
            asSynth02.Stop();
        }
    }

    void ActivarPiano01()
    {
        if(piano01)
        {
            //asPiano01.mute = false;
            asSynth02.Play();
        }
        else
        {
            //asPiano01.mute = true;
            asSynth02.Stop();
        }
    }

    void ActivarPiano02()
    {
        if(piano02)
        {
            //asPiano02.mute = false;
            asSynth02.Play();
        }
        else
        {
            //asPiano02.mute = true;
            asSynth02.Stop();
        }
    }
}