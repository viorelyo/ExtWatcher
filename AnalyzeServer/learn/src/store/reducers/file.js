export const getDocuments = () => {
  fetch("/movies").then(response =>
    response.json().then(data => {
      console.log(data);
    })
  );
};
