using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers;

public class NoteController : Controller
{
    private readonly ApplicationContext _dataBase;

    public NoteController(ApplicationContext dataBase)
    {
        _dataBase = dataBase;
    }

    public IActionResult Index()
    {
        // За работу с представлениями отвечает объект ViewResult.
        // Он производит рендеринг представления в веб-страницу и возвращает ее в виде ответа клиенту.
        // Вызов метода View возвращает объект ViewResult.
        // Затем уже ViewResult производит рендеринг определенного представления в ответ.
        // По умолчанию контроллер производит поиск представления в проекте по следующимy пути - /Views/Имя_контроллера/Имя_метода.cshtml
        return View();
    }

    public IActionResult ShowNotes()
    {
        var notes = _dataBase.Notes.ToList();
        // Метод View имеет перегрузки и он может принимать объект, с помощью этого мы передаем данные на страницу и указываем какую страницу будем грузить
        return View("ShowNotes", notes);
    }

    public IActionResult ShowScreenAddNote()
    {
        // Можно не указывать какую страницу грузить и он автоматически подтянет название с название метода
        return View();
    }

    public IActionResult ShowNoteInfo(int noteId)
    {
        var note = _dataBase.Notes.FirstOrDefault(note => note.Id == noteId);
        return View(note);
    }

    public IActionResult AddNote(string title, string text)
    {
        var note = new Note
        {
            Text = text,
            Title = title
        };
        _dataBase.Notes.Add(note);
        _dataBase.SaveChanges();
        return ShowNotes();
    }

    public IActionResult UpdateNote(string title, string text, int noteId)
    {
        var note = _dataBase.Notes.First(note => note.Id == noteId);

        note.Title = title;
        note.Text = text;

        _dataBase.SaveChanges();
        return ShowNotes();
    }

    public IActionResult DeleteNote(int noteId)
    {
        var note = _dataBase.Notes.FirstOrDefault(note => note.Id == noteId);
        if (note != null)
        {
            _dataBase.Notes.Remove(note);
            _dataBase.SaveChanges();
        }

        return ShowNotes();
    }
}