using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public Question[] questions;
    public static List<Question> unansweredQuestions;
    private Question currentQuestion;
    [SerializeField]
    private Text factText;
    [SerializeField]
    private Text trueAnswerText;
    [SerializeField]
    private Text falseAnswerText;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float timeBetweenQuestions = 1f;
    void Start(){
        if(unansweredQuestions == null || unansweredQuestions.Count == 0){
            unansweredQuestions = questions.ToList<Question>();
        }
        SetCurrentCuestion();
        // Debug.Log(currentQuestion.fact + " is " + currentQuestion.isTrue);
    }
    void SetCurrentCuestion()
    {   
        int randomQuestionIndex = UnityEngine.Random.Range(0, unansweredQuestions.Count);
        currentQuestion = unansweredQuestions[randomQuestionIndex];
        factText.text = currentQuestion.fact;
        if(currentQuestion.isTrue){
            trueAnswerText.text = "CORRECT";
            falseAnswerText.text = "WRONG";
        } else {
            trueAnswerText.text = "WRONG";
            falseAnswerText.text = "CORRECT";
        }
    }
    IEnumerator TransitionToNextQuestion(){
        unansweredQuestions.Remove(currentQuestion);
        yield return new WaitForSeconds(timeBetweenQuestions);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void UserSelectTrue(){
        animator.SetTrigger("True"); 
        if(currentQuestion.isTrue){
            Debug.Log("Correct!");
        }else{
            Debug.Log("Wrong!");
        }
        StartCoroutine(TransitionToNextQuestion());
    }
    public void UserSelectFalse(){
        animator.SetTrigger("False"); 
        if(!currentQuestion.isTrue){
            Debug.Log("Correct!");
        }else{
            Debug.Log("Wrong!");
        }
        StartCoroutine(TransitionToNextQuestion());

    }
}
