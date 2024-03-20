function hideshow() {
    var p = document.getElementById("addp");
    if (p.style.display === "none") {
      p.style.display = "block";
    } else {
      p.style.display = "none";
    }
  }

  function showform() {
    var form = document.getElementById("form");
    if (form.style.display === "none") {
      form.style.display = "block";
    } else {
      form.style.display = "none";
    }
  }

  function add() {
    var fnum = parseFloat(document.getElementById("firstnumber").value);
    var snum = parseFloat(document.getElementById("secondnumber").value);

    var sum=fnum+snum;
    var resultp=document.getElementById("resultp");

    if(isNaN(fnum) || isNaN(snum)){
        resultp.textContent="Enter valid numbers please";
    }
    else{
        resultp.textContent="The sum of "+fnum+ " and "+snum+" is "  + sum;
    }
    
 
  }

